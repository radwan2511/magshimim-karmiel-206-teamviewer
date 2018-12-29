#pragma once
#include <iostream>
#include <string>

//from std library 
using std::string;
using std::cout;
using std::endl;
using std::cin;

class Test
{
public:
	void vaildLoginAndPasswrod(string username, string password); 
	void invaildLoginAndPasswrod(string username, string password);
	void emptyLogin(string username);
	void emptyPassword(string password);
	void emptyLoginAndPassword(string username, string password);
	void isUserExsits(string username);
};
