namespace Vegetarians_Assistant.Services.ModelView;
public class DishNutritionalView
{
    public string Name { get; set; }

    public decimal? TotalWeights { get; set; } = 0;

    public decimal? TotalCalories { get; set; } = 0;
    public decimal? TotalProtein { get; set; } = 0;
    public decimal? TotalCarbs { get; set; } = 0;
    public decimal? TotalFat { get; set; } = 0;
    public decimal? TotalFiber { get; set; } = 0;
    public decimal? TotalVitaminA { get; set; } = 0;
    public decimal? TotalVitaminB { get; set; } = 0;
    public decimal? TotalVitaminC { get; set; } = 0;
    public decimal? TotalVitaminD { get; set; } = 0;
    public decimal? TotalVitaminE { get; set; } = 0;
    public decimal? TotalCalcium { get; set; } = 0;
    public decimal? TotalIron { get; set; } = 0;
    public decimal? TotalMagnesium { get; set; } = 0;
    public decimal? TotalOmega3 { get; set; } = 0;
    public decimal? TotalSugars { get; set; } = 0;
    public decimal? TotalCholesterol { get; set; } = 0;
    public decimal? TotalSodium { get; set; } = 0;
    public decimal? TotalNutrients => TotalCalories + TotalProtein + TotalCarbs + TotalFat + TotalFiber +
                                   TotalVitaminA + TotalVitaminB + TotalVitaminC + TotalVitaminD +
                                   TotalVitaminE + TotalCalcium + TotalIron + TotalMagnesium +
                                   TotalOmega3 + TotalSugars + TotalCholesterol + TotalSodium;
}
