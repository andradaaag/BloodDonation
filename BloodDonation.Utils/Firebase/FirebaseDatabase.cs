using System;
using System.Net.Http;

namespace BloodDonation.Utils.Firebase
{
    public class FirebaseDatabase
    {
        private string RootNode { get; set; }

        public FirebaseDatabase(string baseUrl)
        {
            RootNode = baseUrl;
        }

        public FirebaseDatabase Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            }

            return new FirebaseDatabase(RootNode + '/' + node);
        }

        public FirebaseDatabase NodePath(string nodePath)
        {
            return new FirebaseDatabase(RootNode + '/' + nodePath);
        }

        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, RootNode).Execute();
        }

        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, RootNode, jsonData).Execute();
        }

        public FirebaseResponse Post(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Post, RootNode, jsonData).Execute();
        }

        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), RootNode, jsonData).Execute();
        }

        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, RootNode).Execute();
        }

        public override string ToString()
        {
            return RootNode;
        }
    }
}