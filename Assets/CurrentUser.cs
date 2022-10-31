using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class CurrentUser : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    DatabaseReference dbReference;
    public GameObject scoreTextDB;
    FirebaseAuth auth;
    FirebaseUser user;

    void Start()
    {
        // Get the root reference location of the database.
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        writeNewUser(user.UserId, user.DisplayName, ScoreSystem.theScore);
        //StartCoroutine(UpdateScore(ScoreSystem.theScore));
    }

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void Update()
    {
        if(CollectCoins.updateScore)
            StartCoroutine(UpdateScore(ScoreSystem.theScore));
    }

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                Debug.Log("Signed in " + user.DisplayName);
            }
        }
    }

    private void writeNewUser(string userId, string name, int scoreDB)
    {
        // Creating a class object of a public class called User
        UserDBClass userDB = new UserDBClass(name, scoreDB);

        // Parsing our class object as a JSON file
        string jsonDB = JsonUtility.ToJson(userDB);

        // Now pass all this information to the Database
        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(jsonDB);
    }

    private IEnumerator UpdateScore(int score)
    {
        //Set the currently logged in user xp
        var DBTask = dbReference.Child("users").Child(user.UserId).Child("scoreDB").SetValueAsync(score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Score is Now Updated
            Debug.Log("Score is Updated");
            CollectCoins.updateScore = false;
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
