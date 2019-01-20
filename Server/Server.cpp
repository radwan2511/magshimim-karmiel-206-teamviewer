#include "Server.h"

using namespace std;

queue<string> myClients;
std::mutex m;
int cnt = 0;
fstream f;
std::condition_variable con;

string fileData = "";

class Helper;

Server::Server()
{
	// notice that we step out to the global namespace
	// for the resolution of the function socket

	// this server use TCP. that why SOCK_STREAM & IPPROTO_TCP
	// if the server use UDP we will use: SOCK_DGRAM & IPPROTO_UDP
	_serverSocket = ::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	if (_serverSocket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__ " - socket");
}

Server::~Server()
{
	try
	{
		// the only use of the destructor should be for freeing 
		// resources that was allocated in the constructor
		::closesocket(_serverSocket);
	}
	catch (...) {}
}

void Server::serve(int port)
{

	struct sockaddr_in sa = { 0 };

	sa.sin_port = htons(port); // port that server will listen for
	sa.sin_family = AF_INET;   // must be AF_INET
	sa.sin_addr.s_addr = INADDR_ANY;    // when there are few ip's for the machine. We will use always "INADDR_ANY"


										// again stepping out to the global namespace
										// Connects between the socket and the configuration (port and etc..)
	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - bind");

	// Start listening for incoming requests of clients
	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - listen");
	cout << "Listening on port " << port << endl;

	// starting the thread to handle the recieved messages
	thread ts(&Server::handleRecievedMessages, this);
	ts.detach();

	while (true)
	{
		// the main thread is only accepting clients 
		// and add then to the list of handlers
		cout << "Waiting for client connection request" << endl;

		accept();

	}
}

void Server::accept()
{
	// this accepts the client and create a specific socket from server to this client
	SOCKET client_socket = ::accept(_serverSocket, NULL, NULL);

	if (client_socket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__);

	cout << "Client accepted. Server and client can speak" << endl;
	
	std::thread tr(&Server::clientHandler, this, client_socket);
	tr.detach();

}

void Server::clientHandler(SOCKET clientSocket)
{
	int MTC = 0;
	string n = "";
	string data;
	string st;
	string currUser;
	string nextUser;
	queue<string> temp;
	std::unique_lock<std::mutex> fLock(m, defer_lock);

	while (clientSocket)
	{
		try
		{
			MTC = Helper::getMessageTypeCode(clientSocket);
			RecievedMessage* rcv = new RecievedMessage(clientSocket, MTC);
			addRecievedMessage(rcv);

		}
		catch (...)
		{
			closesocket(clientSocket);
		}
	}
}

/*
The function handles client log in request
input: RecievedMessage
output: user
*/
bool Server::handleLogIn(RecievedMessage * msg)
{
	vector<string> vec = msg->getValues();
	User* newUser = nullptr;
	if (this->_db.isUserAndPassMatch(vec[0], vec[1]))
	{
		newUser = getUserByName(vec[0]);
		if (newUser == nullptr)
		{
			Helper::sendData(msg->getSock(), "206");
			newUser = new User(vec[0], msg->getSock());
			this->_connectedUsers.emplace(msg->getSock(), newUser);
			msg->setUser(newUser);
		}
		else
		{
			Helper::sendData(msg->getSock(), "209");
		}
	}
	else
	{
		Helper::sendData(msg->getSock(), "209");
	}

	return newUser;
}

/*
this function handle client sign up request and return bool if could sign up or not
input: RecievedMessage
output: bool
*/

bool Server::handleSignUp(RecievedMessage * msg)
{
	vector<string> vec = msg->getValues();
	if (Validator::isUsernameValid(vec[0]))
	{
		if (Validator::isPasswordValid(vec[1]))
		{
			if (!this->_db.isUserExists(vec[0]))
			{
				this->_db.addNewUser(vec[0], vec[1], vec[2]);
				Helper::sendData(msg->getSock(), "206"); // sends a success that the user signed up
				return true;
			}
			else
			{
				Helper::sendData(msg->getSock(), "209");//sends a failure that the user is'nt exists
			}
		}
		else
		{
			Helper::sendData(msg->getSock(), "1041");//sends a failure 
		}
	}
	else
	{
		Helper::sendData(msg->getSock(), "1043");
	}
	return false;

}

/*
The fucntion returns user by name 
input: name - the name of the user
output: *User - the user of the requested name
*/
User * Server::getUserByName(string name)
{
	for (auto& x : _connectedUsers)
	{
		if (x.second->getUsername() == name)
		{
			return x.second;
		}
	}
	return nullptr;
}

/*
The function builds the recevid message
input: Socket - the socket of the client that massage was recived 
	   messageCode - the recived massage from the client
output: RecievedMessage - the built massage 
*/
RecievedMessage * Server::buildRecievedMessage(SOCKET socket, int messageCode)
{
	return new RecievedMessage(socket, messageCode);
}

/*
The fucntion handles the recived massages from the clients
input: void
output: void
*/
void Server::handleRecievedMessages()
{
	//SOCKET saved ;
	std::unique_lock<std::mutex> locker(_mtxRecievedMessages);
	static Room *saved = NULL;
	while (true)
	{
		if (_queRcvMessages.empty())
		{
			_cond.wait(locker);
		}
		switch (_queRcvMessages.front()->getMessageCode())
		{
		case LOG_IN:
			handleLogIn(_queRcvMessages.front());
			cout << _queRcvMessages.front()->getUser()->getUsername() <<" signed in!" << endl;
			break;
		case SIGN_OUT:
			handleSignOut(_queRcvMessages.front());
			cout << _queRcvMessages.front()->getUser()->getUsername() <<" Logged Out!!!" << endl;
			break;
		case SIGN_UP:
			handleSignUp(_queRcvMessages.front());
			cout << "We got a new user :D" << endl;
			break;
		case ROOM_JOIN:
			//handleJoinRoom(_queRcvMessages.front());
			//cout << _queRcvMessages.front()->getUser()->getUsername() << " joined the room!" << endl;
			Helper::sendData(_queRcvMessages.front()->getSock(), "206");
			saved = _queRcvMessages.front()->getUser()->getRoom();
			handleControl(saved, _queRcvMessages.front());
			break;
		case ROOM_LEAVE:
			handleLeaveRoom(_queRcvMessages.front());
			break;
		case ROOM_CREATE:
			handleCreateRoom(_queRcvMessages.front());
			cout << _queRcvMessages.front()->getUser()->getUsername() << "created a room"<< endl;
			cout << "Room number is - " <<  _queRcvMessages.front()->getUser()->getRoom()->getId() << endl;
			cout << "Room type is - " << _queRcvMessages.front()->getUser()->getRoom()->getType() << endl;
			cout << "Room max users count is -" << _queRcvMessages.front()->getUser()->getRoom()->getMaxUsers() << endl;
			saved = _queRcvMessages.front()->getUser()->getRoom();
			handleControl(saved, _queRcvMessages.front());
			break;
		case MOUSE_MOVE:
			handleControl(saved,_queRcvMessages.front());
			break;
		default:
			handleSignOut(_queRcvMessages.front());
			cout << _queRcvMessages.front()->getUser()->getUsername() << "Signed out !!" << endl;
			break;		
		}
		_queRcvMessages.pop();
	}
}

/*
The function adds the recevid message to the reseved message queue and notfy the recevid message handler
input: recived message
output: void
*/
void Server::addRecievedMessage(RecievedMessage * rcvMessage)
{
	//_queRcvMessages.push(rcvMessage);

	std::unique_lock<std::mutex> locker(_mtxRecievedMessages);
	rcvMessage->setUser(getUserBySocket(rcvMessage->getSock()));
	//locker.lock();
	_queRcvMessages.push(rcvMessage);
	locker.unlock();
	_cond.notify_one();


}

/*
return user by socket
input: socket
output: user
*/
User * Server::getUserBySocket(SOCKET socket)
{
	for (auto& x : this->_connectedUsers)
	{
		if (x.first == socket)
		{
			return x.second;
		}
	}
	return nullptr;
}

/*
this function create room by the client request
input: RecievedMessage
output: bool
*/
bool Server::handleCreateRoom(RecievedMessage * msg)
{
	User* user = msg->getUser();
	vector<string> vec = msg->getValues();
	if (user != nullptr)
	{
		if (user->createRoom(_roomIdSequence, vec[0], stoi(vec[1]) ))
		{
			this->_roomList.emplace(_roomIdSequence, user->getRoom());
			_roomIdSequence++;
			return true;
		}
	}
	return false;
}

/*
this function close the room
input: RecievedMessage
output: bool
*/
bool Server::handleCloseRoom(RecievedMessage * msg)
{
	User* user = msg->getUser();
	int roomId = user->closeRoom();
	if (roomId != -1)
	{
		this->_roomList.erase(roomId);
		this->_roomIdSequence--;
		return true;
	}
	return false;
}

/*
this function handle the client join room request and return if it worked
input: RecievedMessage
output: bool
*/
bool Server::handleJoinRoom(RecievedMessage * msg)
{
	User* user = msg->getUser();
	int roomId = stoi(msg->getValues()[0]);
	Room* room = getRoomById(roomId);
	if (user != nullptr)
	{
		if (room != nullptr)
		{
			if (room->getMaxUsers() > (int)room->getUsers().size())
			{
				user->joinRoom(room);
				return room->joinRoom(user);
			}
			else
			{
				Helper::sendData(msg->getSock(), "1101");
				return false;
			}
		}
	}
	Helper::sendData(msg->getSock(), "1102");
	return false;
}

/*
return the room by id
input: id
output: room
*/
Room * Server::getRoomById(int id)
{
	return this->_roomList[id];
}

void Server::handleControl(Room* saved, RecievedMessage * msg)
{
	//string data;
	vector<User*> u ;
	//get the values into a new vector for easier use 
	for (int i = 0; i<saved->getUsers().size(); i++)
		u.push_back((saved->getUsers())[i]);
	auto s = u[0]->getSocket();
	//u.emplace_back(s);


	
	// send for admin (user 1) users 2 ip
	string adminIp;
	if (u.size() == 1)
	{
		Helper::sendData(u[0]->getSocket(),"admin");
		cout << "only one client connected " << endl;
	}


	if (u.size() == 2)
	{
		Helper::sendData(u[1]->getSocket(), "reciver");
		adminIp = "127.0.0.1";//Helper::getStringPartFromSocket(u[1]->getSocket(), 15);
		Helper::sendData(u[0]->getSocket(), adminIp);
		cout << "Two clients connected !!!" << endl;

	}
	
}

/*
this function will sign out the client
input : received message
output: void
*/
void Server::handleSignOut(RecievedMessage * msg)
{
	if (msg->getUser() != nullptr)
	{
		this->_connectedUsers.erase(msg->getSock());

		if (msg->getUser()->getRoom() != nullptr)
		{
			handleCloseRoom(msg);
		}
		handleLeaveRoom(msg);
	}

}

/*
this function handle the client leave room request and return if it worked
input: RecievedMessage
output: bool
*/
bool Server::handleLeaveRoom(RecievedMessage * msg)
{
	User* user = msg->getUser();
	user->leaveRoom();
	return true;
}

