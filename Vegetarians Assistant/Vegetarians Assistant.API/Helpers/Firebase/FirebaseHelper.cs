
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Vegetarians_Assistant.API.Helpers.Firebase;
public class FirebaseHelper : IFirebaseHelper
{
    private FirebaseApp? firebaseApp;

    public FirebaseApp? InitializeFirebase()
    {
        try
        {
            if (firebaseApp == null)
            {
                // Path to the service account key file
                var serviceAccountPath = Path.Combine(Directory.GetCurrentDirectory(), "credentials.json");

                firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(serviceAccountPath)
                });
            }
            return firebaseApp;
        }
        catch (Exception)
        {

            return null;
        }
    }

}
