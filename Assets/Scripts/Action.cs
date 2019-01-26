using UnityEngine;
using System.Collections;

public interface Action {
    void Execute(GameObject actor, Item target);
}

public delegate void ItemAction(GameObject actor, Item item);

public delegate void PlayerAction(Player player, Item item);

public class ActionInstance : Action {
    private readonly ItemAction action;

    public ActionInstance(ItemAction action) {
        this.action = action;
    }

    public void Execute(GameObject actor, Item target) {
        action(actor, target);
    }
}

public static class ActionEx {
    public static PlayerAction AsPlayerAction(this Action action) {
        return (p, i) => action.Execute(p.gameObject, i);
    }
}