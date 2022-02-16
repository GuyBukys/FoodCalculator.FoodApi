namespace FoodCalculator.FoodApi.RepositoryModel;

public class NutritionDataResponse
{
    public TotalNutrients totalNutrients { get; set; }
}

public abstract class NutrientBase
{
    public string label { get; set; }
    public double quantity { get; set; }
    public string unit { get; set; }
}

public class TotalNutrients
{
    public ENERCKCAL ENERC_KCAL { get; set; }
    public FAT FAT { get; set; }
    public CHOCDF CHOCDF { get; set; }
    public FIBTG FIBTG { get; set; }
    public SUGAR SUGAR { get; set; }
    public PROCNT PROCNT { get; set; }
}

public class CHOCDF : NutrientBase
{
}

public class FIBTG : NutrientBase
{
}

public class SUGAR : NutrientBase
{
}

public class PROCNT : NutrientBase
{
}
public class ENERCKCAL : NutrientBase
{
}

public class FAT : NutrientBase
{
}