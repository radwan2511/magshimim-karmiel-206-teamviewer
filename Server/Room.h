#pragma once
#include <WinSock2.h>
#include <Windows.h>
#include "Helper.h"
#include <queue>
#include <fstream>
#include <thread>
#include <mutex>
#include <map>
#include <vector>
#include "User.h"

class User;

using namespace std;

class Room
{
private:
	vector<User*> _users;
	User* _admin;
	int _maxUsers;
	string _type;
	int _id;
public:
	Room(int id, User* admin, string type, int maxUsers);
	string getUsersListMessage();
	void sendMessage(User* excludeUser, string message);
	void sendMessage(string message);
	bool joinRoom(User* user);
	void leaveRoom(User* user);
	int closeRoom(User* user);
	int getId();
	string getType();
	int getMaxUsers();
	vector<User*> getUsers();
};
