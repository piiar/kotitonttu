using System;
using UnityEngine;

public class BowlScript : MonoBehaviour {
    public BowlType Bowltype;

    private GameObject filler;
    private MeshRenderer fillerRenderer;
    private bool hasContent;

    public void Start() {
        filler = transform.GetChild(0).gameObject;
        fillerRenderer = filler.GetComponent<MeshRenderer>();

        hasContent = true;
        setVisibility();
    }

    private void setVisibility() {
        fillerRenderer.enabled = hasContent;
    }

    public void CatAction(Creature cat, Item item) {
        if (hasContent) {
            Debug.Log("Cat consumes " + Bowltype + "!");
            hasContent = false;
            setVisibility();
        }
        else {
            Debug.Log(Bowltype + "bowl is empty!");
            cat.SetNewGoal();
        }
    }

    public void PlayerAction(Player player, Item item) {
        if (hasContent) {
            return;
        }

        if (Bowltype == BowlType.Food && player.HasItem(ItemType.Food)) {
            fillAndRemovePlayerItem(player);
        }
        if (Bowltype == BowlType.Water && player.HasItem(ItemType.Water)) {
            fillAndRemovePlayerItem(player);
        }
    }

    private void fillAndRemovePlayerItem(Player player) {
        hasContent = true;
        setVisibility();

        GameObject.Destroy(player.GetCarriedItem().gameObject);
        player.SetCarriedItem(null);
    }
}

public enum BowlType {
    Food,
    Water
}