using System;

class Debt
{
    /* Add Mortgage, Auto, Student, Personal
     * */

    protected double initamount, percentage;
    protected string type, name, bank;
    protected bool ismonthlypayment, isyearlypayment;
    protected DateTime programstartdate;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public double InitAmount
    {
        get
        {
            return initamount;
        }
    }

    public Debt()
    {
    }

    public Debt(string debtname, string debttype, double debtamount)
    {
        name = debtname;
        type = debttype;
        initamount = debtamount;
    }
}
