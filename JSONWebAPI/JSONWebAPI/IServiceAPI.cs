using System.Data;

namespace JSONWebAPI
{
    public interface IServiceAPI
    {
        int CreateNewAccount(string nick, string passwordAcc, string chatName);
        DataTable addChatForUser(int id, string pass);
        int chatAuthentication(string passsword);

        DataTable addChatForUser2(int id, string pass);
        DataTable GetUserDetails(int id);
        DataTable UserAuthentication(string nick, string pass);
        // bool UserAuthentication(string userName, string pass);
        DataTable getChatContent(int id, int ifAdmin);
        DataTable getChatList(int id);

        DataTable checkAnswers(int id, int ifAdmin);

        void passRequest(int chatID, string content, string from);
        void updateRequest(int chatID, int requestID, int whatAnswer);



    }
}