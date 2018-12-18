#include "DataBase.h"
#include <iostream>

unordered_map<string, vector<string>> results;

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
	results.clear();

	st = "select * from users";
	rc = sqlite3_exec(_db, st.c_str(), callback, 0, &zErrMsg);
	
	size = results["users"].size();
	for (int j = 0; j < size; j++)
	{
		_usernames.emplace_back(results["username"][j]);
		_passwords.emplace_back(results["password"][j]);
	}

	results.clear();

	st = "select * from password";
	rc = sqlite3_exec(_db, st.c_str(), callback, 0, &zErrMsg);

	/*size = results["question"].size();
	for (int j = 0; j < size; j++)
	{

		_questions.emplace_back(new Question(stoi(results["question_id"][j]) , results["question"][j] , results["correct_ans"][j] , results["ans2"][j], results["ans3"][j], results["ans4"][j]));

	}*/
	//std::cout << results.begin << std::endl;

	results.clear();

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
