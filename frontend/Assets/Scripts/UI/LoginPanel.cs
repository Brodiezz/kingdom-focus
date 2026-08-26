using UnityEngine;
using TMPro;
using System.Collections;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField kingdomNameInput;
    [SerializeField] private TMP_InputField heroNameInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI messageText;
    
    private bool isRegisterMode = false;
    
    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton.onClick.AddListener(OnRegisterClick);
        toggleButton.onClick.AddListener(ToggleMode);
        
        SetLoginMode();
    }
    
    private void OnLoginClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Please enter username and password", Color.red);
            return;
        }
        
        StartCoroutine(LoginCoroutine(username, password));
    }
    
    private IEnumerator LoginCoroutine(string username, string password)
    {
        loginButton.interactable = false;
        ShowMessage("Logging in...", Color.yellow);
        
        yield return StartCoroutine(ApiService.Instance.Login(username, password, response => {
            if (response.success)
            {
                // Parse token and userId from response
                var json = JsonUtility.FromJson<LoginResponse>(response.data);
                ApiService.Instance.SetAuthToken(json.token, json.user.id);
                
                ShowMessage("Login successful! Loading kingdom...", Color.green);
                // Load main game scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
            else
            {
                ShowMessage($"Login failed: {response.error}", Color.red);
                loginButton.interactable = true;
            }
        }));
    }
    
    private void OnRegisterClick()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;
        string kingdomName = kingdomNameInput.text;
        string heroName = heroNameInput.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Please fill all required fields", Color.red);
            return;
        }
        
        StartCoroutine(RegisterCoroutine(username, email, password, kingdomName, heroName));
    }
    
    private IEnumerator RegisterCoroutine(string username, string email, string password, string kingdomName, string heroName)
    {
        registerButton.interactable = false;
        ShowMessage("Creating account...", Color.yellow);
        
        yield return StartCoroutine(ApiService.Instance.Register(username, email, password, kingdomName, heroName, response => {
            if (response.success)
            {
                var json = JsonUtility.FromJson<LoginResponse>(response.data);
                ApiService.Instance.SetAuthToken(json.token, json.user.id);
                
                ShowMessage("Account created! Welcome to Kingdom Focus!", Color.green);
                // Load main game scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
            else
            {
                ShowMessage($"Registration failed: {response.error}", Color.red);
                registerButton.interactable = true;
            }
        }));
    }
    
    private void ToggleMode()
    {
        isRegisterMode = !isRegisterMode;
        if (isRegisterMode)
        {
            SetRegisterMode();
        }
        else
        {
            SetLoginMode();
        }
    }
    
    private void SetLoginMode()
    {
        isRegisterMode = false;
        titleText.text = "Login to Kingdom Focus";
        emailInput.gameObject.SetActive(false);
        kingdomNameInput.gameObject.SetActive(false);
        heroNameInput.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(true);
        registerButton.gameObject.SetActive(false);
        toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Create Account";
    }
    
    private void SetRegisterMode()
    {
        isRegisterMode = true;
        titleText.text = "Create Your Kingdom";
        emailInput.gameObject.SetActive(true);
        kingdomNameInput.gameObject.SetActive(true);
        heroNameInput.gameObject.SetActive(true);
        loginButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(true);
        toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Login Instead";
    }
    
    private void ShowMessage(string message, Color color)
    {
        messageText.text = message;
        messageText.color = color;
    }
}

[System.Serializable]
public class LoginResponse
{
    public string token;
    public UserData user;
}

[System.Serializable]
public class UserData
{
    public int id;
    public string username;
    public string email;
}
