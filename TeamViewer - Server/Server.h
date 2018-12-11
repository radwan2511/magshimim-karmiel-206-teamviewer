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
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <sstream>
#include <string>

using std::cout;

class Server
{
public:
	Server();
	~Server();
	void serve(int port);

private:

	void accept();
	void clientHandler(SOCKET clientSocket);
	void RecivesTxtFile(SOCKET clientSocket);// the function recives a text file from the first client 
	void SendsTxtFile(SOCKET clientSocket, string text); //the function sends a text file to the second client 

	SOCKET _serverSocket;

};

