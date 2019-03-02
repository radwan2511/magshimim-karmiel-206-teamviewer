#include "RecievedMessage.h"

/*
this function build the RecievedMessage
INPUT : SOCKET socket, int messageCode
OUTPUT : void
*/
RecievedMessage::RecievedMessage(SOCKET socket, int messageCode)
{
	this->_sock = socket;
	this->_messageCode = messageCode;
	this->_user = nullptr;
	if (messageCode == 203)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
	}
	if (messageCode == 200)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
	}
	if (messageCode == 213)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 1));
	}
	if (messageCode == 207)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 4));
	}
	if (messageCode == 209)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 4));
	}
	if (messageCode == 219)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 1));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 2));
	}
	if (messageCode == 220)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 3));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, 3));
	}
	if(messageCode == 260)
	{
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 2)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 1)));
		this->_values.emplace_back(Helper::getStringPartFromSocket(this->_sock, Helper::getIntPartFromSocket(this->_sock, 1)));
		//this->_values.emplace_back("JPEG");
		//this->_values.emplace_back("23:15:11");
	}
}

/*
this function build the RecievedMessage
INPUT : SOCKET socket, int messageCode, vector<string> values
OUTPUT : void
*/
RecievedMessage::RecievedMessage(SOCKET socket, int messageCode, vector<string> values)
{
	this->_sock = socket;
	this->_messageCode = messageCode;
	this->_user = nullptr;
	this->_values = values;
}


/*
this function return the socket
INPUT : void
OUTPUT : socket
*/
SOCKET RecievedMessage::getSock()
{
	return this->_sock;
}

/*
return the user
INPUT : void
OUTPUT : user
*/
User * RecievedMessage::getUser()
{
	return this->_user;
}

/*
change the user to new user
INPUT : void
OUTPUT : void
*/
void RecievedMessage::setUser(User * newUser)
{
	this->_user = newUser;
}

/*
return MessageCode
INPUT : void
OUTPUT : MessageCode
*/
int RecievedMessage::getMessageCode()
{
	return this->_messageCode;
}

/*
return Values
INPUT : void
OUTPUT : Values
*/
vector<string>& RecievedMessage::getValues()
{
	return this->_values;
}
