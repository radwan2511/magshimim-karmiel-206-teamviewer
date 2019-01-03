#include "Test.h"
#include "DataBase.h"

//Googletest ?
DataBase db;

void Test::vaildLoginAndPasswrod(string username, string password)
{
	if (db.isUserAndPassMatch(username,password))
	{
		cout << "The Login and password are correct :D" << endl;
	}
	else
	{
		cout << "The Login and password don't match!!!" << endl;
	}
}
