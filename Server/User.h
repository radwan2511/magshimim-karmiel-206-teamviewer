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
#include "Room.h"

class Room;
class Game;

using namespace std;

class User
{
private:
	string _username;
	Room *_currRoom;
	Game * _currGame;
	SOCKET _sock;
public:
	User(string username, SOCKET sock);
	~User();
	void send(string message);
	bool createRoom(int roomId, string roomName, int maxUsers);
	bool joinRoom(Room* newRoom);
	void leaveRoom();
	int closeRoom();
	void clearRoom();
	//getters
	string getUsername();
	SOCKET getSocket();
	Room* getRoom();
	Game* getGame();
	//setters
	void setGame(Game* gm);
	void clearGame();
};

