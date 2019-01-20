#include "Room.h"


/*
room constractor
input : int id, User* admin, string name, int maxUsers, int questionTime, int questionNo
output: void
*/
Room::Room(int id, User* admin, string type, int maxUsers)
{
	this->_id = id;
	this->_admin = admin;
	this->_maxUsers = maxUsers;
	this->_type = type;
	this->_users.emplace_back(admin);
}

/*
return UsersListMessage
input : void
output: string
*/
string Room::getUsersListMessage()
{
	string st = "";
	st = "108" + st + to_string(this->_users.size());
	string s;
	for (int i = 0; i < this->_users.size(); i++)
	{
		s = to_string(this->_users[i]->getUsername().length());
		while (s.length() < 2)
		{
			s = "0" + s;
		}
		st = st + s + (this->_users[i]->getUsername());
	}
	return st;

}

/*
send message to all users in room without excludeUser
input : User * excludeUser, string message
output: void
*/
void Room::sendMessage(User * excludeUser, string message)
{
	for (int i = 0; i < this->_users.size(); i++)
	{
		if (this->_users[i] != excludeUser)
		{
			this->_users[i]->send(message);
		}
	}
}

/*
send message to all users in room
input : string message
output: void
*/
void Room::sendMessage(string message)
{
	this->sendMessage(nullptr, message);
}

/*
join the user to the room
input : User * user
output: bool
*/
bool Room::joinRoom(User * user)
{
	if (this->_users.size() < this->_maxUsers)
	{
		this->_users.emplace_back(user);
		string st = "206";
		user->send(st);
		for (int i = 0; i < this->_users.size(); i++)
		{
			this->sendMessage(this->getUsersListMessage());
		}
		return true;
	}
	else
	{
		return false;
	}
}

/*
leave the user from the room
input : User * user
output: void
*/
void Room::leaveRoom(User * user)
{
	for (int i = 0; i < this->_users.size(); i++)
	{
		if (this->_users[i] == user)
		{
			this->_users[i]->send("1120");
			this->_users.erase(this->_users.begin() + i);
		}
	}

	for (int i = 0; i < this->_users.size(); i++)
	{
		this->sendMessage(this->getUsersListMessage());
	}
}

/*
close the user room
input : User * user
output: room id
*/
int Room::closeRoom(User * user)
{
	if (this->_admin == user)
	{
		for (int i = 0; i < this->_users.size(); i++)
		{
			/*if (this->_users[i] != this->_admin)
			{
				this->_users[i]->clearRoom();
			}*/
			this->_users[i]->clearRoom();
			this->_users[i]->send("116");
		}
		
		return this->_id;
	}
	else
	{
		return -1;
	}
}

/*
return id
input : void
output: room id
*/
int Room::getId()
{
	return this->_id;
}

/*
return room name
input : void
output: room name
*/
string Room::getType()
{
	return this->_type;
}


/*
return MaxUsers numbers
input : void
output : MaxUsers numbers
*/
int Room::getMaxUsers()
{
	return this->_maxUsers;
}

/*
return Users vector
input : void
output : vector<User*>
*/
vector<User*> Room::getUsers()
{
	return this->_users;
}

