using System;
using System.Collections.Generic;

class Program
{
    // Class Initialization
    static List<Purchase> UserPurchase = new List<Purchase>();
    static List<Bill> UserBill = new List<Bill>();
    static List<Debt> UserDebt = new List<Debt>();
    static List<CreditCard> UserCreditCard = new List<CreditCard>();
    static List<BankAccounts> UserBankAccount = new List<BankAccounts>();
    static List<WantPurchase> UserWantPurchase = new List<WantPurchase>();
    static List<Budget> UserBudget = new List<Budget>();
    static List<ChargeCard> UserChargeCard = new List<ChargeCard>();
    static List<DayProfile> DayBalance = new List<DayProfile>();

    static void Main()
    {
        int select1 = 0;
        //DateTime startDate = DateTime.Today;
        //int endDate = 30;
        //startDate = startDate.AddDays(30);
        //DisplayBalance(startDate, endDate);

        Console.WriteLine("1. Setup Budget\n2. Use Default\n3. to Close Application");
        int.TryParse(Console.ReadLine(), out select1);
        if (select1 == 1)
        {
            Setup();
        }
        else
        {
            RunDefault();
        }

        while (select1 < 3)
        {
            Console.WriteLine("\t1. to add expenses\n\t2. to display\n\t3. to exit");
            int.TryParse(Console.ReadLine(), out select1);
            switch (select1)
            {
                case 1:
                    AddProperties();
                    break;
                case 2:
                    DisplayProcessing();
                    Reset();
                    break;
            }
        }
        //Console.ReadLine();
    }

    static void ClearAll()
    {
        UserPurchase.Clear();
        UserBill.Clear();
        UserDebt.Clear();
        UserCreditCard.Clear();
        UserBankAccount.Clear();
        UserWantPurchase.Clear();
        UserBudget.Clear();
        UserChargeCard.Clear();
        DayBalance.Clear();
    }

    static void Reset()
    {
        for (int i = 0; i < UserBankAccount.Count; i++)
        {
            UserBankAccount[i].ClearBalance();
        }

        for (int i = 0; i < UserCreditCard.Count; i++)
        {
            UserCreditCard[i].ClearBalance();
        }

        DateTime AccStart = new DateTime(2013, 4, 25);
        for (int i = 0; i < UserChargeCard.Count; i++)
        {
            UserChargeCard[i].ClearBalance();
            UserChargeCard[i].ProgramStartDate = AccStart;
        }

        for (int i = 0; i < UserWantPurchase.Count; i++)
        {
            if (UserWantPurchase[i].Date.CompareTo(AccStart) <= 0)
            {
                UserWantPurchase[i].ExpensePurchase(UserWantPurchase[i].Date);
            }
        }
    }

    static void Setup()
    {
        /* Define User and associated properties */
        int income1, income2, day1, day2, day3, day4;
        string temp;
        double money1, money2, pretax = 0;

        /* Input Income (for future paycheck)
         * Input Checking Account information (Name and balance)
         * Input Credit Card Information (Name and Balance)
         * 
         *  */
        Console.WriteLine("Please enter your gross income: ");
        int.TryParse(Console.ReadLine(), out income1);
        Console.WriteLine("What day of the month is the paycheck 1: ");
        int.TryParse(Console.ReadLine(), out day1);
        Console.WriteLine("What day of the month is the paycheck 2: ");
        int.TryParse(Console.ReadLine(), out day2);
        Console.WriteLine("Please enter your spouses income: ");
        int.TryParse(Console.ReadLine(), out income2);
        Console.WriteLine("What day of the month is the paycheck 1: ");
        int.TryParse(Console.ReadLine(), out day3);
        Console.WriteLine("What day of the month is the paycheck 2: ");
        int.TryParse(Console.ReadLine(), out day4);
        char yes = 'n';
        Console.WriteLine("Do you have any pretax monthly deductions? (Like health premiums)");
        char.TryParse(Console.ReadLine(), out yes);
        do
        {
            if (yes == 'y')
            {
                Console.Write("How much: ");
                double pt;
                double.TryParse(Console.ReadLine(), out pt);
                pretax += pt;
                Console.WriteLine("Do you have any more pretax monthly deductions? (Like health premiums)");
                char.TryParse(Console.ReadLine(), out yes);
            }
        } while (yes != 'n');
        pretax *= 12;
        Budget U1 = new Budget(income1, day1, day2, income2, day3, day4, UserBankAccount[PaymentSource("What Bank to you want to pay to?")], pretax);
        UserBudget.Add(U1);


        Console.WriteLine("Enter bank name: ");
        temp = Console.ReadLine();
        Console.WriteLine("Enter your current bank account balance: ");
        double.TryParse(Console.ReadLine(), out money1);
        BankAccounts BC = new BankAccounts(temp, money1);
        UserBankAccount.Add(BC);

        Console.WriteLine("Enter credit card name: ");
        temp = Console.ReadLine();
        Console.WriteLine("Enter credit limit: ");
        double.TryParse(Console.ReadLine(), out money1);
        Console.WriteLine("Enter current balance: ");
        double.TryParse(Console.ReadLine(), out money2);
        Console.WriteLine("Enter statement end day: ");
        int.TryParse(Console.ReadLine(), out day1);
        DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day1);
        CreditCard CC = new CreditCard(temp, money2, money1, dt);
        UserCreditCard.Add(CC);

        Console.WriteLine("Enter charge card name: ");
        temp = Console.ReadLine();
        Console.WriteLine("Enter credit limit: ");
        double.TryParse(Console.ReadLine(), out money1);
        Console.WriteLine("Enter current balance: ");
        double.TryParse(Console.ReadLine(), out money2);
        Console.WriteLine("Enter statement end day: ");
        int.TryParse(Console.ReadLine(), out day1);
        Console.WriteLine("Enter statement due day: ");
        int.TryParse(Console.ReadLine(), out day2);
        DateTime endday, dueday;

        if (day1 > DateTime.Today.Day)
        {
            endday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day1);
            endday = endday.AddMonths(1);
            dueday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day2);
            dueday = dueday.AddMonths(2);
        }
        else 
        {
            endday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day1);
            dueday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day2);
            dueday = dueday.AddMonths(1);
        }
        
        ChargeCard CHC = new ChargeCard(temp, money2, money1, endday, dueday);
        UserCreditCard.Add(CC);

    }

    static void RunDefault()
    {

    }

    static void AddProperties()
    {
        int select, d, m, y, daysinmonth;
        double cost;
        string temp1, temp2;
        Console.WriteLine("What type of Expense do you have?");
        Console.WriteLine("\t1. Bill\n\t2. Purchase\n\t3. Debt Payment");
        int.TryParse(Console.ReadLine(), out select);
        switch (select)
        {
            case 1:
                Console.WriteLine("What is the name of the bill?");
                temp1 = Console.ReadLine();
                Console.WriteLine("How much is the bill?");
                double.TryParse(Console.ReadLine(), out cost);
                Console.WriteLine("What day of the month is the bill due?");
                int.TryParse(Console.ReadLine(), out select);
                AddBill(temp1, cost, select);
                
                break;
            case 2:
                Console.WriteLine("Need (1) or Want (2)?");
                int.TryParse(Console.ReadLine(), out select);
                Console.WriteLine("What is the name of the purchase?");
                temp1 = Console.ReadLine();
                Console.WriteLine("Where did you purchase the item?");
                temp2 = Console.ReadLine();
                Console.WriteLine("How much was the item?");
                double.TryParse(Console.ReadLine(), out cost);
                
                do
                {
                    Console.WriteLine("When did you purchase the item?\nYear: ");
                    int.TryParse(Console.ReadLine(), out y);
                    Console.WriteLine("Month: ");
                    int.TryParse(Console.ReadLine(), out m);
                    Console.WriteLine("Day: ");
                    int.TryParse(Console.ReadLine(), out d);
                    daysinmonth = DateTime.DaysInMonth(y, m);

                    if ((d <= 0) || (d > daysinmonth))
                    {
                        Console.WriteLine("Not a valid input");
                    }

                } while ((d <= 0) || (d > daysinmonth));
                DateTime wantdate = new DateTime(y, m, d);

                switch (select)
                {
                    case 1:
                        AddPurchase(temp1, temp2, cost, wantdate);
                        break;
                    case 2:
                        AddWantPurchase(temp1, temp2, cost, wantdate);
                        break;
                }
                break;
            case 3:
                PayDebt();
                break;
        }
    }

    static void AddBill(string name, double cost, int dueday)
    {
        int select, acc;
        do
        {
            acc = DisplayBankCreditCharge();
            int.TryParse(Console.ReadLine(), out select);
            select--;
            if ((select >= 0) && (select < UserBankAccount.Count))
            {
                Bill UB = new Bill(name, cost, dueday, UserBankAccount[select]);
                UserBill.Add(UB);
            }
            else if ((select >= UserBankAccount.Count) && (select < (acc - UserChargeCard.Count)))
            {
                Bill UB = new Bill(name, cost, dueday, UserCreditCard[select - UserBankAccount.Count]);
                UserBill.Add(UB);
            }
            else if ((select >= (UserBankAccount.Count + UserCreditCard.Count)) && (select < acc))
            {
                Bill UB = new Bill(name, cost, dueday, UserChargeCard[select - UserBankAccount.Count - UserCreditCard.Count]);
                UserBill.Add(UB);
            }

            if ((select < 0) || (select >= acc))
            {
                Console.WriteLine("Invalid input");
            }

        } while ((select < 0) || (select >= acc));
    }

    static void AddPurchase(string PurchaseName, string Store, double Cost, DateTime PurchaseDate)
    {
        int select;
        int acc = DisplayBankCreditCharge();
        int.TryParse(Console.ReadLine(), out select);
        select--;
        if ((select >= 0) && (select < UserBankAccount.Count))
        {
            Purchase Purch = new Purchase(PurchaseName, Store, Cost, UserBankAccount[select], PurchaseDate);
            UserPurchase.Add(Purch);
        }
        else if ((select >= UserBankAccount.Count) && (select < (acc - UserChargeCard.Count)))
        {
            Purchase Purch = new Purchase(PurchaseName, Store, Cost, UserCreditCard[select - UserBankAccount.Count], PurchaseDate);
            UserPurchase.Add(Purch);
        }
        else if ((select >= (UserBankAccount.Count + UserCreditCard.Count)) && (select < acc))
        {
            Purchase Purch = new Purchase(PurchaseName, Store, Cost, UserChargeCard[select - UserBankAccount.Count - UserCreditCard.Count], PurchaseDate);
            UserPurchase.Add(Purch);
        }
    }

    static void AddWantPurchase(string PurchaseName, string Store, double Cost, DateTime PurchaseDate)
    {
        int select, acc;
        do
        {
            acc = DisplayBankCreditCharge();
            int.TryParse(Console.ReadLine(), out select);
            select--;
            if ((select >= 0) && (select < UserBankAccount.Count))
            {
                WantPurchase Purch = new WantPurchase(PurchaseName, Store, Cost, UserBankAccount[select], PurchaseDate);
                UserWantPurchase.Add(Purch);
            }
            else if ((select >= UserBankAccount.Count) && (select < (acc - UserChargeCard.Count)))
            {
                WantPurchase Purch = new WantPurchase(PurchaseName, Store, Cost, UserCreditCard[select - UserBankAccount.Count], PurchaseDate);
                UserWantPurchase.Add(Purch);
            }
            else if ((select >= (UserBankAccount.Count + UserCreditCard.Count)) && (select < acc))
            {
                WantPurchase Purch = new WantPurchase(PurchaseName, Store, Cost, UserChargeCard[select - UserBankAccount.Count - UserCreditCard.Count], PurchaseDate);
                UserWantPurchase.Add(Purch);
            }

            if ((select < 0) || (select >= acc))
            {
                Console.WriteLine("Invalid input");
            }

        } while ((select < 0) || (select >= acc));
        
    }

    static int DisplayBankCreditCharge()
    {
        int acc = 0;
        Console.WriteLine("Select Account to paid from");
        for (int i = 0; i < UserBankAccount.Count; i++)
        {
            Console.WriteLine("{0}. {1}", (acc + 1), UserBankAccount[i].Name);
            acc++;
        }
        for (int i = 0; i < UserCreditCard.Count; i++)
        {
            Console.WriteLine("{0}. {1}", (acc + 1), UserCreditCard[i].Name);
            acc++;
        }
        for (int i = 0; i < UserChargeCard.Count; i++)
        {
            Console.WriteLine("{0}. {1}", (acc + 1), UserChargeCard[i].Name);
            acc++;
        }
        return acc;
    }

    static void PayDebt()
    {
        Console.WriteLine("Make payment to:");
        Console.WriteLine("1. Credit Card\n2. Charge Card\n3. Other Debt");
        int select, select1 = 0;
        int.TryParse(Console.ReadLine(), out select);
        switch (select)
        {
            case 1:
                do
                {
                    for (int i = 0; i < UserCreditCard.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", (i + 1), UserCreditCard[i].Name);
                    }
                    int.TryParse(Console.ReadLine(), out select1);
                    select1--;
                    if ((select1 < 0) || (select1 >= UserCreditCard.Count))
                    {
                        Console.WriteLine("Not a Valid Input");
                    }
                } while ((select1 < 0) || (select1 >= UserCreditCard.Count));
                UserCreditCard[select1].Payment(PaymentAmount(), PaymentDate());
                break;
            case 2:
                do
                {
                    for (int i = 0; i < UserChargeCard.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", (i + 1), UserChargeCard[i].Name);
                    }
                    int.TryParse(Console.ReadLine(), out select1);
                    select1--;
                    if ((select1 < 0) || (select1 >= UserChargeCard.Count))
                    {
                        Console.WriteLine("Not a Valid Input");
                    }
                } while ((select1 < 0) || (select1 >= UserChargeCard.Count));
                UserChargeCard[select1].Payment(PaymentAmount(), UserBankAccount[PaymentSource("What account do you want to pay from?")], PaymentDate());
                break;
            default:
                break;
        }
    }

    static int PaymentSource(string phrase)
    {
        int select = 0;
        Console.WriteLine(phrase);
        do 
        {
            for (int i = 0; i < UserBankAccount.Count; i++)
            {
                Console.WriteLine("{0}. {1}", (i+1), UserBankAccount[i].Name);
            }
            int.TryParse(Console.ReadLine(), out select);
            select--;

        } while((select < 0) || (select >= UserBankAccount.Count));
        return select;
    }

    static double PaymentAmount()
    {
        Console.WriteLine("How much do you want to pay?");
        double amount = 0;
        double.TryParse(Console.ReadLine(), out amount);
        return amount;
    }

    static DateTime PaymentDate()
    {
        int y, m, d, daysinmonth;
        do
        {
            Console.WriteLine("When do you want to make the payment?\nYear: ");
            int.TryParse(Console.ReadLine(), out y);
            do
            {
                Console.WriteLine("Month: ");
                int.TryParse(Console.ReadLine(), out m);
            } while ((m < 1) || (m > 12));
            Console.WriteLine("Day: ");
            int.TryParse(Console.ReadLine(), out d);
            daysinmonth = DateTime.DaysInMonth(y, m);

            if ((d <= 0) || (d > daysinmonth))
            {
                Console.WriteLine("Not a valid input");
            }

        } while ((d <= 0) || (d > daysinmonth));
        DateTime paymentdate = new DateTime(y, m, d);
        return paymentdate;
    }

    static void DisplayProcessing()
    {
        List<double> balance = new List<double>();
        DateTime Date1 = new DateTime(2013, 5, 10);
        DateTime newDate;
        DateTime MonthAhead = Date1.AddMonths(1);
        string[] dayname = {"Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Monday", "Tuesday"};
        string[] monthname = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        
        
        // Select starting date of display and how many days you want to display.
        int d, m, y, daysinmonth;
        do 
        {
            Console.WriteLine("What date do you want to start at?\nYear: ");
            int.TryParse(Console.ReadLine(), out y);
            Console.WriteLine("Month: ");
            int.TryParse(Console.ReadLine(), out m);
            Console.WriteLine("Day: ");
            int.TryParse(Console.ReadLine(), out d);
            daysinmonth = DateTime.DaysInMonth(y, m);

            if ((d <= 0) || (d > daysinmonth))
            {
                Console.WriteLine("Not a valid input");
            }

        } while ((d <= 0) || (d > daysinmonth)) ;
        
        DateTime startdate = new DateTime(y, m, d);

        
        Console.WriteLine("How many days ahead do you want to look?");
        int dahead;
        int.TryParse(Console.ReadLine(), out dahead);

        TimeSpan diff = startdate.Subtract(Date1);
        int diffdays = diff.Days;

        // Setup DayProfiles before selected date and then display when at selected date.
        for (int i = 0; i < (diffdays+dahead); i++)
        {
            newDate = Date1.AddDays(i);

            // Add Bills to selected Checking Account Balance
            for (int j = 0; j < UserBill.Count; j++)
            {
                if (UserBill[j].DayofMonth == newDate.Day)
                {
                    UserBill[j].ExpensePurchase(newDate);
                }
            }

            // Add Want Purchases
            for (int j = 0; j < UserWantPurchase.Count; j++)
            {
                if (UserWantPurchase[j].Date == newDate)
                {
                    UserWantPurchase[j].ExpensePurchase(newDate);
                }
            }

            // Add Need Purchases
            for (int j = 0; j < UserPurchase.Count; j++)
            {
                if (UserPurchase[j].PurchaseDate == newDate)
                {
                    UserPurchase[j].ExpensePurchase(newDate);
                }
            }

            // Add Charge Card Purchases and Payoff 
            for (int j = 0 ; j < UserChargeCard.Count; j++)
            {
                if (UserChargeCard[j].DueDay == newDate.Day)
                {
                    if (UserChargeCard[j].HasDefault)
                    {
                        UserChargeCard[j].Payoff(newDate);
                    } 
                }
            }

            // Add Paychecks
            for (int j = 0; j < UserBudget.Count; j++)
            {
                UserBudget[j].Paycheck(newDate);
            }

            // If Bank Balance has changed since last day, store new value, 
            // else store most recent balance
            for (int j = 0; j < UserBankAccount.Count; j++)
            {
                for (int h = 0; h < UserBankAccount[j].BankBalance.Count; h++)
                {
                    if (UserBankAccount[j].BankBalance[h].BalanceDate == newDate)
                    {
                        balance.Add(UserBankAccount[j].BankBalance[h].AccBalance);
                    }
                }
            }
            DayProfile day = new DayProfile(balance[balance.Count - 1], newDate);
            DayBalance.Add(day);
        }

        // Display Balances
        DisplayBalance(startdate, dahead);
    }

    static void DisplayBalance(DateTime startDate, int numdays)
    {
        Console.WriteLine("\n\n\n");
        int dayoffset = 0;
        double income = 0, expenses = 0;
        string[] dayname = { "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Monday", "Tuesday" };
        string[] monthname = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        DateTime MonthAhead = startDate.AddMonths(1);

        // Find Balance day to start at
        for (int i = 0; i < DayBalance.Count; i++)
        {
            if (startDate == DayBalance[i].BalanceDate)
            {
                dayoffset = i;
            }
        }

        for (int i = 0; i < numdays; i++)
        {
            DateTime newDate = startDate.AddDays(i);

            Console.WriteLine("{0}, {1} {2}", newDate.DayOfWeek, monthname[newDate.Month - 1], newDate.Day);

            // Add Bills to selected Checking Account Balance
            for (int j = 0; j < UserBill.Count; j++)
            {
                if (UserBill[j].DayofMonth == newDate.Day)
                {
                    Console.WriteLine("{0} is due with the amount of ${1}", UserBill[j].Name, UserBill[j].Cost);
                    expenses = expenses + UserBill[j].Cost;
                }
            }

            // Display all Purchases
            for (int j = 0; j < UserWantPurchase.Count; j++)
            {
                if (UserWantPurchase[j].Date == newDate)
                {
                    Console.WriteLine("Bought {0} from {1} for ${2}.", UserWantPurchase[j].Name, UserWantPurchase[j].Store, UserWantPurchase[j].Cost);
                    //expenses = expenses + UserWantPurchase[j].Cost;
                }
            }

            // Add Charge Card Purchases and Payoff 
            for (int j = 0; j < UserChargeCard.Count; j++)
            {
                if (UserChargeCard[j].DueDay == newDate.Day)
                {
                    Console.WriteLine("{0} is due with the amount of ${1}", UserChargeCard[j].Name, UserChargeCard[j].GetEndBalance(newDate));
                    //expenses = expenses + UserChargeCard[j].GetEndBalance(newDate);
                }
            }

            // Add Paychecks
            for (int j = 0; j < UserBudget.Count; j++)
            {
                if ((UserBudget[j].PayDay11 == newDate.Day) || (UserBudget[j].PayDay12 == newDate.Day))
                {
                    Console.WriteLine("GOT PAYED: {0}", UserBudget[j].Paycheck1);
                    income = income + UserBudget[j].Paycheck1;
                }
                if ((UserBudget[j].PayDay21 == newDate.Day) || (UserBudget[j].PayDay22 == newDate.Day))
                {
                    Console.WriteLine("GOT PAYED: {0}", UserBudget[j].Paycheck2);
                    income = income + UserBudget[j].Paycheck2;
                }
            }

            if (newDate == DayBalance[i + dayoffset].BalanceDate)
            {
                Console.WriteLine("Account Balance: {0}\n", DayBalance[i + dayoffset].Balance);
            }

            if (newDate == MonthAhead)
            {
                Console.WriteLine("Total income over the last month: {0}", income);
                Console.WriteLine("Total expenses over the last month: {0}", expenses);
                Console.WriteLine("Net Income: {0}\n", income - expenses);
                income = 0;
                expenses = 0;
                MonthAhead = MonthAhead.AddMonths(1);
            }
        }
    }
}