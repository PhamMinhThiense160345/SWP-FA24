using FirebaseAdmin;

namespace Vegetarians_Assistant.API.Helpers.Firebase;

public interface IFirebaseHelper
{
    FirebaseApp? InitializeFirebase();
}
