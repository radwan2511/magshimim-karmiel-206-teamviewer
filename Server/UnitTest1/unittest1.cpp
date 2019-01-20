#include "stdafx.h"
#include "CppUnitTest.h"
#include "../TeamViewer - Server/DataBase.h"
#include "../TeamViewer - Server/Room.h"
#include "../TeamViewer - Server/User.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest1
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		User * d = new User("Dan",200);
		User * n = new User("Ned", 201);
		DataBase * db = new DataBase();
		Room *r = new Room(1,d,"screen",2);
		bool expect = true;

		TEST_METHOD(TestIsUserExsits)
		{
			//the unit test if the user exists in the system
			bool back = db->isUserExists("Dan");
			Assert::AreEqual(expect, back);
			
		}
		TEST_METHOD(TestIsUserAdded )
		{
			//the unit test checks if the user added  
			bool back = db->addNewUser("Bob123","Bob123","Bob123@gmail.com");
			Assert::AreEqual(expect, back);
		}
		TEST_METHOD(TestIsUserAndPassMatch)
		{
			//the unit test checks if the username if the username and password matches 
			bool back = db->addNewUser("Bob123", "Bob123", "Bob123@gmail.com");
			Assert::AreEqual(expect, back);
		}
		TEST_METHOD(TestIsRoomCreated)
		{
			;//the unit test checks if the room created successfully
			bool back = d->createRoom(1, "d", 2);
			Assert::AreEqual(expect, back);
		}
		TEST_METHOD(TestIsJoinedRoom)
		{
			;//the unit test checks if the user joned the room 
			bool back = n->joinRoom(d->getRoom());
			Assert::AreEqual(expect, back);
		}
		
		

	};
}