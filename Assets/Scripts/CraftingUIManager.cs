using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CraftingUIManager : MonoBehaviour
{
    [Header("Tab Panels")]
    public GameObject craftingPanel;
    public GameObject recipesPanel;

    [Header("Recipe Page Controls")]
    public Button prevButton;
    public Button nextButton;
    
    [Header("Tab Buttons")]
    public Button craftingTabButton;
    public Button recipesTabButton;

    [System.Serializable]
    public class RecipeSlot
    {
        public GameObject slotObject;
        public Image resultImage;
        public Image collectibleImage_1;
        public Image collectibleImage_2;
    }

    [Header("Recipe Slots (2 per page)")]
    public List<RecipeSlot> recipeSlots;
    
    private Dictionary<(Collectible.CollectibleType, Collectible.CollectibleType), InventoryManager.itemType> recipes;
    private List<(Collectible.CollectibleType, Collectible.CollectibleType, InventoryManager.itemType)> flatRecipeList;

    private int currentPage = 0;
    private const int recipesPerPage = 2;
    
    void Start()
    {
        recipes = CraftingManager.Instance.recipes;
        flatRecipeList = recipes.Select(r => (r.Key.Item1, r.Key.Item2, r.Value)).ToList();
        UpdateRecipePage();
        
        craftingTabButton.interactable = true;
        recipesTabButton.interactable = false;
    }

    public void ShowCraftingPanel()
    {
        craftingPanel.SetActive(true);
        recipesPanel.SetActive(false);

        craftingTabButton.interactable = false;
        recipesTabButton.interactable = true;
    }

    public void ShowRecipesPanel()
    {
        craftingPanel.SetActive(false);
        recipesPanel.SetActive(true);
        UpdateRecipePage();
        
        craftingTabButton.interactable = true;
        recipesTabButton.interactable = false;
    }

    public void NextPage()
    {
        if ((currentPage + 1) * recipesPerPage < flatRecipeList.Count)
        {
            currentPage++;
            UpdateRecipePage();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateRecipePage();
        }
    }

    private void UpdateRecipePage()
    {
        int startIndex = currentPage * recipesPerPage;

        for (int i = 0; i < recipesPerPage; i++)
        {
            int recipeIndex = startIndex + i;

            if (recipeIndex < flatRecipeList.Count)
            {
                var (a, b, result) = flatRecipeList[recipeIndex];
                var slot = recipeSlots[i];

                slot.resultImage.sprite = CraftingManager.Instance.resultSprites[(int)result];

                slot.collectibleImage_1.sprite = CraftingManager.Instance.collectibleSprites[(int)a];

                slot.collectibleImage_2.sprite = CraftingManager.Instance.collectibleSprites[(int)b];

                slot.slotObject.SetActive(true);
            }
            else
            {
                recipeSlots[i].slotObject.SetActive(false);
            }
        }

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage + 1) * recipesPerPage < flatRecipeList.Count;
    }
}
