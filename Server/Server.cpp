#include "Server.h"
#include "Helper.h"
#include "DataBase.h"
#include "sqlite3.h"

#define SIZE 1000 
#define TXT_FILE "text"
#define DEFAULT_BUFLEN 1024
#define SUCCESS "206"
#define SIGN_UP "101"
#define LOG_IN "100"
#define FAIL "200"
#define LEN_CHECK 2 


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
	
	clientHandler(client_socket);
	std::thread tr(&Server::clientHandler, this, client_socket);
	tr.detach();

}

void Server::clientHandler(SOCKET clientSocket)
{
	string data;
	Helper h;
	while (clientSocket)
	{
		try
		{
			//DataBase* db = new DataBase();
			data = h.getStringPartFromSocket(clientSocket, SIZE);
			data.erase(std::remove(data.begin(), data.end(),'Í'), data.end());
			cout << data << endl;
			if (data.substr(0,3) == SIGN_UP)
			{
				data = data.substr(3, data.size() - 3);
				if(handleSignup(data))
				{
					data = SUCCESS;
				}
				else
				{
					data = FAIL;
				}
			}
			else if (data.substr(0, 3) == LOG_IN)
			{
				data = data.substr(3, data.size() - 3);
				if (handleLogIn(data))
				{
					data = SUCCESS;
				}
				else
				{
					data = FAIL;
				}
			}
			Helper::sendData(clientSocket, data);
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
bool Server::handleLogIn(string data)
{
	DataBase *db = new DataBase();
	int userLen = atoi((data.substr(0, LEN_CHECK)).c_str());
	string username = data.substr(LEN_CHECK, userLen);
	if (db->isUserExists(username))
	{
		int passwordLen = atoi((data.substr(LEN_CHECK + userLen, LEN_CHECK)).c_str());
		string password = data.substr(LEN_CHECK + userLen + LEN_CHECK, passwordLen);
		if (db->isUserAndPassMatch(username, password))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	else
	{
		return false;
	}
}

/*
this function handle client sign up request and return bool if could sign up or not
input: RecievedMessage
output: bool
*/

bool Server::handleSignup(string data)
{
	DataBase *db = new DataBase();
	int userLen = atoi((data.substr(0, LEN_CHECK)).c_str());
	string username = data.substr(LEN_CHECK, userLen);
	if (db->isUserExists(username))
	{
			return false;
	}
	else
	{
		int passwordLen = atoi((data.substr(LEN_CHECK + userLen, LEN_CHECK)).c_str());
		string password = data.substr(LEN_CHECK + userLen + LEN_CHECK, passwordLen);
		int emailLen = atoi((data.substr(LEN_CHECK + userLen + LEN_CHECK + passwordLen, LEN_CHECK)).c_str());
		string email = data.substr(LEN_CHECK + userLen + LEN_CHECK + passwordLen + LEN_CHECK, emailLen);
		if(db->addNewUser(username,password,email))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	

}