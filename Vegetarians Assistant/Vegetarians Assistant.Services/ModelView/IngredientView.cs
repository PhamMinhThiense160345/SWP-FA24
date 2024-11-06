
namespace Vegetarians_Assistant.Services.ModelView;
public record AddIngredientView(int IngredientId, int DishId, decimal Weight);
public record UpdateIngredientView(int IngredientId, int DishId, decimal NewWeight);
