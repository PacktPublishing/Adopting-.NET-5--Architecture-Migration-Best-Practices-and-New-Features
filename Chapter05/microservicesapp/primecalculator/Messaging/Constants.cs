using System;

namespace primecalculator.Messaging
{
    public static class Constants
    {
        internal const string MANUAL_DEBUGTIME_RABBITMQ_CS = "localhost:5672:guest:guest"; //Only used when running without Tye

        public const string RABBIT_HOST = "SERVICE__RABBITMQ__MQ_BINDING__HOST";
        public const string RABBIT_PORT = "SERVICE__RABBITMQ__MQ_BINDING__PORT";
        public const string RABBIT_ALT_HOST = "SERVICE__RABBITMQ__HOST";
        public const string RABBIT_ALT2_HOST_FROM_TYE_K8S_FIXED = "rabbitmq"; //Matches exactly the name from tye.yaml and rabbitmq.yaml
        public const string RABBIT_ALT_PORT = "SERVICE__RABBITMQ__PORT";
        public const string RABBIT_ALT2_PORT = "RABBITMQ_SERVICE_PORT"; //Comes to the container when deployed to K8s pod

        public const string RABBIT_USER = "RABBIT_USER";
        public const string RABBIT_PSWD = "RABBIT_PSWD";
        public const string RABBIT_QUEUE = "RABBIT_QUEUE";

        public static string GetRabbitMQHostName()
        {
            /*Parsing via tye's service uri way but without using tye.config.extension*/
            //var uri = configuration.GetServiceUri("rabbitmq"); var endpoint = new AmqpTcpEndpoint(uri);

            var v = Environment.GetEnvironmentVariable(Constants.RABBIT_HOST);
            if (string.IsNullOrWhiteSpace(v))
            {
                v = Environment.GetEnvironmentVariable(Constants.RABBIT_ALT_HOST);
                if (string.IsNullOrWhiteSpace(v))
                    return RABBIT_ALT2_HOST_FROM_TYE_K8S_FIXED;
                else return v;
            }
            else return v;

            /*Parsing via tye's connection string way
            var rmq = configuration.GetConnectionString("rabbitmq:mq_binding") ?? Constants.MANUAL_DEBUGTIME_RABBITMQ_CS;
            string[] values = rmq.Split(":");
            hostname = values[0];
            port = values[1];//*/
        }

        public static string GetRabbitMQPort()
        {
            var v = Environment.GetEnvironmentVariable(Constants.RABBIT_PORT);
            if (string.IsNullOrWhiteSpace(v))
            {
                v = Environment.GetEnvironmentVariable(Constants.RABBIT_ALT_PORT);
                if (string.IsNullOrWhiteSpace(v) || v == "-1")
                    return Environment.GetEnvironmentVariable(Constants.RABBIT_ALT2_PORT);
                else return v;
            }
            else return v;
        }

        public static string GetRabbitMQUser()
        {
            var v = Environment.GetEnvironmentVariable(Constants.RABBIT_USER);
            if (string.IsNullOrWhiteSpace(v))
                return "guest"; //Debug time only - when localmachine debug without containers, without tye
            else return v;
        }

        public static string GetRabbitMQPassword()
        {
            var v = Environment.GetEnvironmentVariable(Constants.RABBIT_PSWD);
            if (string.IsNullOrWhiteSpace(v))
                return "guest"; //Debug time only - when localmachine debug without containers, without tye
            else return v;
        }

        public static string GetRabbitMQQueueName()
        {
            var v = Environment.GetEnvironmentVariable(Constants.RABBIT_QUEUE);
            if (string.IsNullOrWhiteSpace(v))
                return "primes"; //Debug time only - when localmachine debug without containers, without tye
            else return v;
        }
    }
}
