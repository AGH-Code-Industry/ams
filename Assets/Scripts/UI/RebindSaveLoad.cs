using UnityEngine;
using UnityEngine.InputSystem;

/*
    Funkcja pozwala wczytać zapisane rebindy przy użyciu funkcji statycznej LoadRebinds(). 
    Jest ona wywoływana zaraz po stworzeniu obiektu PlayerInputActions w InputManager.

    Funkcja OnDisable() jest wywoływana przy wyłączaniu obiektu, który zawiera ten skrypt.
    Ten skrypt jest podpięty pod obiekt ControlsSettings w scenie o tej samej nazwie, w 
    rezultacie ustawienia są zapisywane do PlayerPrefs po wyjściu z ustawień klawiszy.
*/

public class RebindSaveLoad : MonoBehaviour
{
    public static void LoadRebinds()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds) && InputManager.actions != null) {
            InputManager.actions.LoadBindingOverridesFromJson(rebinds);
        }
        Debug.Log("Bonjour");
    }

    public void OnDisable()
    {
        if (InputManager.actions == null) {
            return;
        }

        var rebinds = InputManager.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        Debug.Log("Bye");
    }
}
