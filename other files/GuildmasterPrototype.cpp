#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int nowPrice;


vector<string> unit = { "전사1", "전사2",  "전사3",  "전사4",
                        "마법사1", "마법사2",  "마법사3",  "마법사4",
                        "궁수1", "궁수2",  "궁수3",  "궁수4",
                        "힐러1", "힐러2",  "힐러3",  "힐러4" };
vector<string> unbidedUnits;

void print(vector<string> const& v)
{
    for (string i : v) {
        cout << i << " ";
    }
    cout << endl << endl;
}

class player 
{
public:
    int money = 500;
    int count = 0;
    int& myMoney = money;
    int& myCount = count;
    vector<string> joindUnits;

    bool bid()
    {
        int bidPrice;
        cout << "입찰 급액 : ";
        cin >> bidPrice;

        // 파티원 다 구했을 때
        if (myCount == 4)
        {
            cout << "파티가 꽉 찼습니다." << endl;
            return false;
        } 

        // 입찰 금액이 0 이하일 때
        else if(bidPrice <= 0)
        {
            cout << "입찰 금액이 맞지 않습니다." << endl;
        }

        // 입찰 금액이 소지 금액보다 적을 때
        else if (myMoney < bidPrice)
        {
            cout << "금액이 부족합니다." << endl;
            return false;
        }    

        // 입찰 금액이 현재 입찰된 금액보다 적을 때
        else if (bidPrice < nowPrice)
        {
            cout << "입찰 금액이 부족합니다." << endl;
            return false;
        }     

        // 입찰 가능할 때
        else
        {
            cout << "입찰했습니다." << endl;
            nowPrice = bidPrice;
        }

        cout << "현재 금액" << nowPrice << endl << endl;
        return true;
    }

};

class auctionManager
{
public:    
    player player1, player2, player3, player4;
    vector<player> players = { player1, player2, player3, player4 };
    
    int playerNum = 0;
    int lastPlayerIndex = 0;
    int timeOfAuction = 0;

    void whoIsLast() 
    {
        lastPlayerIndex = playerNum - 1;
    }

    void printMember()
    {
        cout << 1 << "플레이어의 파티 : ";
        print(players.at(0).joindUnits);

        cout << 2 << "플레이어의 파티 : ";
        print(players.at(1).joindUnits);

        cout << 3 << "플레이어의 파티 : ";
        print(players.at(2).joindUnits);

        cout << 4 << "플레이어의 파티 : ";
        print(players.at(3).joindUnits);

        cout << endl << endl;
    }

    void auction(vector<string> & units)
    {      
        timeOfAuction++;

        cout << timeOfAuction << "차 경매 시작!!" << endl;

        do
        {            
            cout << "이번 순서 : " << *units.begin() << endl;
            cout << "어느 플레이어? : ";
            cin >> playerNum;

            switch (playerNum)
            {
            case 1: // 1번 플레이어
            {
                if(player1.bid())
                    whoIsLast();
                break;
            }
            case 2: // 2번 플레이어
            {
                if (player2.bid())
                    whoIsLast();
                break;
            }
            case 3: // 3번 플레이어
            {
                if (player3.bid())
                    whoIsLast();
                break;
            }
            case 4: // 4번 플레이어
            {
                if (player4.bid())
                    whoIsLast();
                break;
            }
            case 0: // 0을 누르면 경매 종료
            {
                // 유찰
                if (nowPrice == 0)
                {
                    unbidedUnits.push_back(*units.begin());
                    cout << "유찰되었습니다." << endl;
                    cout << "유찰 목록 : ";
                    print(unbidedUnits);

                } 
                // 낙찰
                else
                {
                    players.at(lastPlayerIndex).joindUnits.push_back(*units.begin());
                    players.at(lastPlayerIndex).myMoney -= nowPrice;
                    players.at(lastPlayerIndex).myCount++;

                    printMember();
                    nowPrice = 0;
                }
                units.erase(units.begin() + 0);
            }
            default:
                break;            
            continue;
            }
        } while (!units.empty());

        if (!unbidedUnits.empty())
        {
            auction(unbidedUnits);
        }
        else
            return;
    }
};



int main()
{    
    auctionManager am;
    random_shuffle(unit.begin(), unit.end());
    cout << "경매 순서 : ";
    print(unit);
    am.auction(unit);
    
    cout << "경매 끝!";
}

