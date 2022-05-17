using System;
using System.Collections.Generic;
using System.Linq;

class Cards{
  string[] deckOfCards = new string[52];//deck of cards
  Queue<string> player1cards = new Queue<string>(52);//player1 queue
  Queue<string> player2cards = new Queue<string>(52);//player2 queue
  List<int> listNumbers = new List<int>();// list of the rand numbers
  Queue<string> playerPile1 = new Queue<string>(26);
  Queue<string> playerPile2 = new Queue<string>(26);
  
  public void DECK_OF_CARDS()
  {
    int i = 0;
    while( i < 4)//nested loop to loop 4 times 13 for the 4 suits and 13 cards in each suit
    {
      for(int j = 0; j < 13; j++)
      {
        if(i == 0)//spades
        {
          deckOfCards[j] =  "Spades: " + Convert.ToString(j+1);//the first card which is 1(ace) cant be (j = 0) therefore it is incremented so the cards go from 1-13 instead of 0-12 
        }
        else if(i == 1)//clubs
        {
          deckOfCards[j + 13] =  "Clubs: " + Convert.ToString(j+1);//j resets to 0 so to keep on adding further to the deck of cards j is incremented to the position above the last card that was added
        }
        else if(i == 2)//hearts
        {
          deckOfCards[j + 26] =  "Hearts: " + Convert.ToString(j+1);
        }
        else if(i == 3)//diamonds
        {
          deckOfCards[j + 39] =  "Diamonds: " + Convert.ToString(j+1);
        }
      }
      i++;
    }
  }
  
  public void playerQueues()//Adds shuffled cards into the player's queues
  {
    for(int i = 0; i < 52; i++)
    {
      if (i < 26)//adds the first 26 of the random cards to p1's queue
      {
        player1cards.Enqueue(deckOfCards[listNumbers[i]]);//The random number corresponds to the position of the card which will be added to the player queue 
      }
      else//adds the last 26 of the random cards to p2;s queue
      {
        player2cards.Enqueue(deckOfCards[listNumbers[i]]);
      }  
    }
  }
  
  public void randomNums()//Generates a list of random numbers 0-51
  {
    List<int> possible = Enumerable.Range(0, 52).ToList();//all possible numbers
    Random num = new Random();
    for (int i = 0; i < 52; i++)//loops 52 times to 
    {
      int index = num.Next(0, possible.Count);
      listNumbers.Add(possible[index]);
      possible.RemoveAt(index);
    }
  }
  public bool pileCount()
  {
    bool playing = true;

    if (player1cards.Count == 0 || player2cards.Count == 0)
    {
      playing = false;
    }
    return playing;
  }
  public string p1_placeCard(int turn)
  {
    Console.Write("Player 1 ---> ");
    string card = player1cards.Peek();
    playerPile1.Enqueue(player1cards.Dequeue());
    return card;
    
  }
  public string p2_placeCard(int turn)
  {
    Console.Write("Player 2 ---> ");
    string card = player2cards.Peek();
    playerPile2.Enqueue(player2cards.Dequeue());
    return card;
  }
  public string p1Peek()
  {
    string card = player1cards.Peek();
    return card;
  }
  public string p2Peek()
  {
    string card = player2cards.Peek();
    return card;
  }
  public int compareCards(string key, string p1Card, string p2Card, int turn)
  {
    string[] p1result = p1Card.Split(" ");
    string p1_card_num = p1result[1];
    string[] p2result =p2Card.Split(" ");
    string p2_card_num = p2result[1];

    if(key == "s" && p1_card_num == p2_card_num)
    {
      foreach(string p1cards in playerPile1)
      {
        player1cards.Enqueue(p1cards);
      }
      foreach(string p2cards in playerPile2)
      {
        player1cards.Enqueue(p2cards);
      }
      playerPile1.Clear();
      playerPile2.Clear();
      return turn++;
    }
    else if(key == "l" && p1_card_num == p2_card_num)
    {
      foreach(string p1cards in playerPile1){
        player2cards.Enqueue(p1cards);
      }
      foreach(string p2cards in playerPile2){
        player2cards.Enqueue(p2cards);
      }
      playerPile1.Clear();
      playerPile2.Clear();     
      return turn++;
    }
    else
    {
      Console.WriteLine("Cant call snap!!!");
    }
    return turn;
  }

  public void testing()
  {
    Console.WriteLine("-----------------------------------");
    foreach(string d in player1cards)
    {
      Console.WriteLine(d);
    }
    Console.WriteLine("----------------------------");
    foreach(string d in player2cards)
    {
      Console.WriteLine(d);
    }
    Console.WriteLine("-----------------------------------");
    foreach(string d in playerPile1)
    {
     Console.WriteLine(d);
    }
    Console.WriteLine("------------------------------------");
    foreach(string d in playerPile2)
    {
     Console.WriteLine(d);
    }
    Console.WriteLine("--------------------");
  }
  public string winner(){
    string winner = "";
    if(player1cards.Count == 0 && player2cards.Count == 0)
    {
      winner = "DRAW!!!";
    }
    else if(player2cards.Count == 0 && player1cards.Count != 0){
      winner = "WINNER: PLAYER1!!!";
    }
    else{
      winner = "WINNER: PLAYER2!!!";
    }
    return winner;
  }
}
   
class player{
  public string name;
  public void Name(string newName){
    name = newName;
  }
  public string getName()
  {
    return name;
  }
}

class Program{
  public static void Main (string[] args) {
    Program no = new Program();
    no.intro();
    no.play();
  }
  
  public void intro()
  {
    Console.WriteLine(@"
 _______  _        _______  _______ 
(  ____ \( (    /|(  ___  )(  ____ )
| (    \/|  \  ( || (   ) || (    )|
| (_____ |   \ | || (___) || (____)|
(_____  )| (\ \) ||  ___  ||  _____)
      ) || | \   || (   ) || (      
/\____) || )  \  || )   ( || )      
\_______)|/    )_)|/     \||/       
                                    
");
    Console.WriteLine("Player 1's controls are: \n(s) -->  SNAP \n-------------------------------------\nPlayer2's controls are: \n(l) --> SNAP\n-------------------------------------\n(ENTER)\nTo place both player's cards\n-------------------------------------\nAlso press enter after every click\n-------------------------------------");
  }
  public void play()
  {
    Cards deck = new Cards();
    deck.DECK_OF_CARDS();
    deck.randomNums();
    deck.playerQueues();

    string p1Card = deck.p1Peek();
    string p2Card = deck.p2Peek();
    
    Console.WriteLine("Let the game begin!!!");
    int turn = 0;
   while(deck.pileCount())
   {
     //deck.testing();
     string key = Console.ReadLine();
     if(key == "s" || key == "l")
     { 
       deck.compareCards(key,p1Card,p2Card,turn);
     }
     else if(key == "")
     {
       p1Card = deck.p1Peek();
       p2Card = deck.p2Peek();
       Console.WriteLine(deck.p1_placeCard(turn));
       Console.WriteLine(deck.p2_placeCard(turn));
       turn++;
     }
     else
     {
       Console.WriteLine("Wrong key!!!");
     }
   } 
    Console.WriteLine(deck.winner());
  }
}