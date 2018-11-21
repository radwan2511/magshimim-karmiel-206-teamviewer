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
#include <iostream>

using std::cout;

class Server
{
public:
	Server();
	~Server();
	void serve(int port);
	void clientHandler(SOCKET clientSocket);

private:

	void accept();
	//void clientHandler(SOCKET clientSocket);

	SOCKET _serverSocket;

};

