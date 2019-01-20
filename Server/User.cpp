#include "User.h"

/*
The function is a constractor to User
INPUT : NULL
OUTPUT: NULL
*/

User::User(string username, SOCKET sock)
{
	this->_username = username;
	this->_sock = sock;
	this->_currRoom = nullptr;
}

/*
The function is a destractor to User
INPUT : NULL
OUTPUT: NULL
*/
User::~User()
{
}

/*
The function send the massage to the user
INPUT : message - the massage to send
OUTPUT: NULL
*/
void User::send(string message)
{
	Helper::sendData(this->_sock, message);
}
/*
The function is a getter to the username of the User
INPUT: NULL
OUTPUT: _username - the username of the user
*/
string User::getUsername()
{
	return this->_username;
}
/*
The function is a getter to the socket of the User
INPUT: NULL
OUTPUT: _sock - the socket of the user
*/
SOCKET User::getSocket()
{
	return _sock;
}
Room * User::getRoom()
{
	return this->_currRoom;
}
Game * User::getGame()
{
	return this->_currGame;
}
void User::setGame(Game * gm)
{
	this->_currRoom = nullptr;
	this->_currGame = gm;
}
/*
The function is a setter to Game of the user
INPUT: gm - the game to set
OUTPUT: NULL
*/
//void User::setGame(Game* gm)
//{
//this->_currRoom = nullptr;
//this->_currGame = gm;
//}
/*
The function clears the game
INPUT: NULL
OUTPUT: NULL
*/
void User::clearGame()
{
	this->_currGame = nullptr;
}
/*
The function creates a new Room and returns if succeeded or not
INPUT : roomId- the id of the new room
roomName - the name of the new room
maxUsers - the max amout of user of the room
questionsNo - the number of questions in the room
questionTime - the time per question
OUTPUT : exists - true if the user exsits in the room and false if does'nt
*/
bool User::createRoom(int roomId, string roomName, int maxUsers)
{
	vector<User*> usersToSend;
	bool exists;
	if (this->_currRoom == nullptr) // and if question numbers is not bigger than what we have
	{
		this->_currRoom = new Room(roomId, this, roomName,maxUsers);
		Helper::sendData(this->_sock, "206");
		exists = true;
	}
	else
	{
		Helper::sendData(this->_sock, "209");
		exists = false;
	}
	return exists;
}
/*
The function add User the room and return if succeeded or not
INPUT: newRoom - the room to add the user to
OUTPUT : boolean if succeeded or not to add the user
*/
bool User::joinRoom(Room * newRoom)
{
	if (this->_currRoom == nullptr && newRoom != nullptr)
	{
		this->_currRoom = newRoom;
		return true;
	}
	return false;
}
/*
The function removes the user from the room
INPUT: NULL
OUTPUT : NULL
*/
void User::leaveRoom()
{
	if (this->_currRoom != nullptr)
	{
		this->_currRoom->leaveRoom(this);
		this->_currRoom = nullptr;
	}
}
/*
The function closes the room
INPUT: NULL
OUTPUT : the id of the closed room
*/
int User::closeRoom()
{
	return this->_currRoom->closeRoom(this);
}
/*
The function clears the room
INPUT: NULL
OUTPUT: NULL
*/
void User::clearRoom()
{
	this->_currRoom = nullptr;
}