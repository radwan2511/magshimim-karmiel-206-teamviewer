#include "DataBase.h"
#include <iostream>

unordered_map<string, vector<string>> results;
unordered_map<string, vector<string>> statistics;

/*
constructor for the dataBase
input : void
output: void
*/
DataBase::DataBase()
{
	string st;
	int size;
	rc = sqlite3_open("info.db", &_db);
	if (rc)
	{
		cout << "Can't open database: " << sqlite3_errmsg(_db) << endl;
		sqlite3_close(_db);
		system("pause");
	}
	_usernames.clear();
	_passwords.clear();
	_ipsFrom.clear();
	_ipsTo.clear();
	_files.clear();
	_times.clear();
	results.clear();

	st = "select * from users";
	rc = sqlite3_exec(_db, st.c_str(), callback, 0, &zErrMsg);

	size = results["username"].size();
	for (int j = 0; j < size; j++)
	{
		_usernames.emplace_back(results["username"][j]);
		_passwords.emplace_back(results["password"][j]);
		_emails.emplace_back(results["email"][j]);
	}

	results.clear();
	//gets the statistics

	st = "select * from statistics";
	rc = sqlite3_exec(_db, st.c_str(), statisticsCallback, 0, &zErrMsg);

	size = statistics["From IP"].size();
	for (int j = 0; j < size; j++)
	{
		_ipsFrom.emplace_back(statistics["From IP"][j]);
		_ipsTo.emplace_back(statistics["To IP"][j]);
		_files.emplace_back(statistics["File"][j]);
		_times.emplace_back(statistics["Time"][j]);
	}

	statistics.clear();//clears the results




}

/*
the function calls back the statistics
input : void * notUsed, int argc, char ** argv, char ** azCol
output: int
*/
int DataBase::statisticsCallback(void * notUsed, int argc, char ** argv, char ** azCol)
{
	int i;

	for (i = 0; i < argc; i++)
	{

		auto it = statistics.find(azCol[i]);
		if (it != statistics.end())
		{
			it->second.push_back(argv[i]);
		}
		else
		{
			pair<string, vector<string>> p;
			p.first = azCol[i];

			p.second.push_back(argv[i]);

			statistics.insert(p);
		}
	}

	return 0;
}

bool DataBase::addNewstatistic(string ip_from, string ip_to, string fileType, string time)
{
	string st = "INSERT INTO statistics (FromIP,ToIP,File,Time) VALUES ( '" + ip_from + "','" + ip_to + "','" + fileType + "','" + time + "')";
	rc = sqlite3_exec(_db, st.c_str(), callback, 0, &zErrMsg);
	if (rc != SQLITE_OK)
	{
		return false;
	}

	_ipsFrom.emplace_back(ip_from);
	_ipsTo.emplace_back(ip_to);
	_files.emplace_back(fileType);
	_times.emplace_back(time);

	return true;
}



/*
callback
input : void * notUsed, int argc, char ** argv, char ** azCol
output: int
*/
int DataBase::callback(void * notUsed, int argc, char ** argv, char ** azCol)
{
	int i;

	for (i = 0; i < argc; i++)
	{

		auto it = results.find(azCol[i]);
		if (it != results.end())
		{
			it->second.push_back(argv[i]);
		}
		else
		{
			pair<string, vector<string>> p;
			p.first = azCol[i];

			p.second.push_back(argv[i]);

			results.insert(p);
		}
	}

	return 0;
}


/*
The function checks if the user exists in the DB
input : username - the user name to checks if exists
output: bool - true if the user exists and false if not 
*/
bool DataBase::isUserExists(string username)
{

	for (int i = 0; i < this->_usernames.size(); i++)
	{
		if (username == _usernames[i])
		{
			return true;
		}
	}
	return false;
}
/*
The function adds a new user to the DataBase 
INPUT : username - the username to add 
		password - the password to add 
		email - the email to add
OUTPUT : bool - true if the user added successfully and false if not 
*/
bool DataBase::addNewUser(string username, string password, string email)
{
	string st = "INSERT INTO users (username, password, email) VALUES ( '" + username + "','" + password + "','" + email + "')";
	rc = sqlite3_exec(_db, st.c_str(), callback, 0, &zErrMsg);
	if (rc != SQLITE_OK)
	{
		return false;
	}

	_usernames.emplace_back(username);
	_passwords.emplace_back(password);
	_emails.emplace_back(email);

	return true;
}
/*
The function checks if the entered username and passwrod matches in the DataBase
INPUT : username - the username to check
password - the password to check
OUTPUT : bool - true if the user and password matchs and false if not
*/
bool DataBase::isUserAndPassMatch(string username, string password)
{
	int j = -1;
	for (int i = 0; i < this->_usernames.capacity(); i++)
	{
		if (username == _usernames[i])
		{
			j = i;
			break;
		}
	}
	if (j != -1 && password == this->_passwords[j])
	{
		return true;
	}
	return false;
}
