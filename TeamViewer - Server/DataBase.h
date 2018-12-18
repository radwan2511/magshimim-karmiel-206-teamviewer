#pragma once
#include <string>
#include <unordered_map>
#include <vector>
#include <iostream>
#include "sqlite3.h"
#include <string.h>
#include <sstream>
#include <cstdlib> 
#include <ctime>
#include <map>
#include <math.h>

using std::string;
using std::vector;
using std::unordered_map;
using std::cout;
using std::endl;
using std::map;
using namespace std;

class DataBase
{
private:
	
	sqlite3 * _db;
	vector<string> _usernames;
	vector<string> _passwords;
	
	int rc;
	char *zErrMsg = 0;
	bool flag = true;

public:
	DataBase();

	static int callback(void* notUsed, int argc, char** argv, char** azCol);
};

