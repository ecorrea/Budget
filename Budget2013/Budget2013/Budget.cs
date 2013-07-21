using System;

class Budget
{
    // Add pretax deductions and allow for changing the amount of personal deductions
    // Add IRA/401k Deductions
    // Make additional class for investments
    private int salary1, salary2, payday11, payday12, payday21, payday22;
    private double pretax;
    private BankAccounts Bank;

    public int PayDay11
    {
        get
        {
            return payday11;
        }
    }

    public int PayDay12
    {
        get
        {
            return payday12;
        }
    }

    public int PayDay21
    {
        get
        {
            return payday21;
        }
    }

    public int PayDay22
    {
        get
        {
            return payday22;
        }
    }

    private double paycheck1()
    {
        return netIncome() * salary1 / (salarytot() * 24);
    }

    public double Paycheck1
    {
        get
        {
            return paycheck1();
        }
    }

    private double paycheck2()
    {
        return netIncome() * salary2 / (salarytot() * 24);
    }

    public double Paycheck2
    {
        get
        {
            return paycheck2();
        }
    }

    private double totfed()
    {
        Int16 j = 0;
        for (Int16 i = 0; i < 7; i++)
        {
            if (taxableIncome() > fedTaxLimits[i])
            {
                j = i;
            }
        }
        double tempamount = taxableIncome();
        tempamount = (taxableIncome() - fedTaxLimits[j]) * fedTax[j] + baseFedTax[j];
        return (taxableIncome() - fedTaxLimits[j]) * fedTax[j] + baseFedTax[j];
    }

    private double netIncome()
    {
        return salarytot() - pretax- totfed() - totsoc() - totmed() - totstate();
    }

    public double TaxableIncome
    {
        get
        {
            return taxableIncome();
        }
    }

    private double taxableIncome()
    {
        return salarytot() - pretax - personalexemption - standarddeduction;
    }

    private double totsoc()
    {
        if (taxableIncome() < socialsecTaxLimit)
            return taxableIncome() * socialsectax;
        else
            return socialsecTaxLimit * socialsectax;
    }

    public double Totsoc
    {
        get
        {
            return totsoc();
        }
    }

    private double totmed()
    {
        if (taxableIncome() < medicareTaxLimit)
            return taxableIncome() * medicareTax[0];
        else
            return taxableIncome() * medicareTax[1];
    }

    public double Totmed
    {
        get
        {
            return totmed();
        }
    }

    private double totstate()
    {
        Int16 j = 0;
        for (Int16 i = 0; i < 7; i++)
        {
            if (taxableIncome() > stateTaxLimits[i])
            {
                j = i;
            }
        }
        return (taxableIncome() - stateTaxLimits[j]) * stateTax[j] + baseStateTax[j];
    }

    public double Totstate
    {
        get
        {
            return totstate();
        }
    }

    public double Totfed
    {
        get
        {
            return totfed();
        }
    }

    private int salarytot()
    {
        return salary1 + salary2;
    }

    private double netPaycheck()
    {
        return netIncome() / 24;
    }

    public Budget()
    {
    }

    public Budget(int yearsalary1, int payday_1_1, int payday_1_2, int yearsalary2, int payday_2_1, int payday_2_2, BankAccounts Card, double pt)
    {
        salary1 = yearsalary1;
        payday11 = payday_1_1;
        payday12 = payday_1_2;
        salary2 = yearsalary2;
        payday21 = payday_2_1;
        payday22 = payday_2_2;
        pretax = pt;
        Bank = Card;
    }

    static double[] fedTax = { 0.10, 0.15, 0.25, 0.28, 0.33, 0.35, 0.396 };

    static int[] baseFedTax = { 0, 1785, 9982, 28457, 49919, 107786, 125846 };

    static int[] fedTaxLimits = { 0, 17850, 72500, 146400, 223050, 398350, 450000 };

    static int personalexemption = 3900*3;

    static int standarddeduction = 12200;

    static double socialsectax = 0.0620;

    static int socialsecTaxLimit = 113700;

    static double[] medicareTax = { 0.0145, 0.0235 };

    static int medicareTaxLimit = 250000;

    static double[] stateTax = { 0.01, 0.02, 0.04, 0.06, 0.08, 0.093, 0.103 };

    static int[] baseStateTax = { 0, 142, 533, 1314, 2556, 4118, 181419 };

    static int[] stateTaxLimits = { 0, 14248, 33780, 53314, 74010, 93532, 2000000 };

    public override string ToString()
    {
        return String.Format("Your month paycheck should be: {0}", netPaycheck() * 2);
    }

    public bool Paycheck(DateTime CurrentDate)
    {
        bool isPayDay = false;
        if ((CurrentDate.Day == payday11) || (CurrentDate.Day == payday12))
        {
            Bank.Paycheck(paycheck1(), CurrentDate);
            isPayDay = true;
        }
        if ((CurrentDate.Day == payday21) || (CurrentDate.Day == payday22))
        {
            Bank.Paycheck(paycheck2(), CurrentDate);
            isPayDay = true;
        }
        return isPayDay;
    }

}