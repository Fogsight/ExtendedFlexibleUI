using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : Menu {
    public Button playGame;
    public Button quitGame;

    protected override void OnEnable() {
        base.OnEnable();
        playGame.Select();
        //Pause time, lock controls
    }
    protected override void OnDisable() {
        base.OnDisable();
        //Resume time, unlock controls
    }

    private void Awake() {
        Selectables = new List<Selectable>() {
            playGame,
            quitGame };
    }
}