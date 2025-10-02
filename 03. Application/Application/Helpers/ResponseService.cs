using System.Linq;
using Transversal.Helpers;
using Transversal.Enumerators;
using System.Collections.Generic;

namespace Application.Helpers
{
    public static class ResponseService
    {
        /// <summary>
        /// Generic method that gives a generic response to the api
        /// </summary>
        /// <param name="checkObject">Object to return to the api</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkObject"/> null</param>
        /// <returns></returns>
        public static ResponseServices<T> GenericResponse<T>(T checkObject, Enums.MensajeRespuesta onErrorMessage)
        {
            var response = new ResponseServices<T>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            var success = checkObject != null;

            response.Info = checkObject;
            response.State = success;
            response.Message = success ? Enums.MensajeRespuesta.Ok.ToStringAttribute() : onErrorMessage.ToStringAttribute();
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response to the api using boolean values
        /// </summary>
        /// <param name="checkBool">Bool value to return to the api</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkBool"/> false</param>
        /// <returns></returns>
        public static ResponseServices<bool> GenericResponse(bool checkBool, Enums.MensajeRespuesta onErrorMessage)
        {
            var response = new ResponseServices<bool>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            response.Info = checkBool;
            response.State = checkBool;
            response.Message = checkBool ? Enums.MensajeRespuesta.Ok.ToStringAttribute() : onErrorMessage.ToStringAttribute();
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response to the api
        /// </summary>
        /// <param name="checkObject">List object to return to the api</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkObject"/> null</param>
        /// <returns></returns>
        public static ResponseServices<IEnumerable<T>> GenericResponseArray<T>(IEnumerable<T> checkObject, Enums.MensajeRespuesta onErrorMessage, Enums.MensajeRespuesta type = Enums.MensajeRespuesta.Consulta)
        {
            var response = new ResponseServices<IEnumerable<T>>
            {
                Type = type.ToStringAttribute()
            };

            var success = checkObject != null && checkObject.Any();

            response.Info = checkObject;
            response.State = success;
            response.Message = success ? Enums.MensajeRespuesta.Ok.ToStringAttribute() : onErrorMessage.ToStringAttribute();
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }


        /// <summary>
        /// Generic method that gives a generic response from primitive to the api
        /// </summary>
        /// <param name="checkPrimitive">Primivite variable to return to the api</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkPrimitive"/> null</param>
        /// <returns></returns>
        public static ResponseServices<T> GenericResponsePrimitive<T>(T checkPrimitive, Enums.MensajeRespuesta onErrorMessage) where T : struct
        {
            var response = new ResponseServices<T>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            var success = !(checkPrimitive.Equals(default(int)) || checkPrimitive.Equals(default(float)) || checkPrimitive.Equals(default(long)));

            response.Info = checkPrimitive;
            response.State = success;
            response.Message = success ? Enums.MensajeRespuesta.Ok.ToStringAttribute() : onErrorMessage.ToStringAttribute();
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }
        /// <summary>
        /// Generic method that gives a generic response from primitive to the api
        /// </summary>
        /// <param name="checkPrimitive">Primivite variable to return to the api</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkPrimitive"/> null</param>
        /// <param name="onWarning">Error message on <paramref name="onWarning"/> null</param>
        /// <returns></returns>
        public static ResponseServices<T> GenericResponsePrimitive<T>(T checkPrimitive, Enums.MensajeRespuesta onErrorMessage, string onWarning) where T : struct
        {
            var response = new ResponseServices<T>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            var success = !(checkPrimitive.Equals(default(int)) || checkPrimitive.Equals(default(float)) || checkPrimitive.Equals(default(long)));

            response.Info = checkPrimitive;
            response.State = success;
            response.Message = success ? Enums.MensajeRespuesta.Ok.ToStringAttribute() : onErrorMessage.ToStringAttribute();
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            response.Warning = onWarning;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response to the api
        /// </summary>
        /// <param name="checkObject">Object to return to the api</param>
        /// <param name="onSuccessMessage">Message on success</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkObject"/> null</param>
        /// <param name="warningMessage"></param>
        /// <param name="messageValues">Array of values to replace on success message</param>
        /// <typeparam name="T">Type of object to return.</typeparam>
        /// <returns>ResponseServices type T</returns>
        public static ResponseServices<T> GenericResponse<T>(T checkObject, Enums.MensajeRespuesta onSuccessMessage,
            Enums.MensajeRespuesta onErrorMessage, string warningMessage = null, params string[] messageValues)
            {
            var response = new ResponseServices<T>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute(),
            };

            var success = checkObject != null;

            response.Info = checkObject;
            response.State = success;
            response.Message = success ? BetterMessage(onSuccessMessage, messageValues) : BetterMessage(onErrorMessage, messageValues);
            response.Warning = warningMessage ?? string.Empty;
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response to the api using boolean values
        /// </summary>
        /// <param name="checkBool">Bool value to return to the api</param>
        /// <param name="onSuccessMessage">Success message</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkBool"/> false</param>
        /// <param name="warningMessage"></param>
        /// <param name="messageValues">Array of values to replace on success message</param>
        /// <returns>ResponseServices of type Boolean</returns>
        public static ResponseServices<bool> GenericResponse(bool checkBool, Enums.MensajeRespuesta onSuccessMessage,
            Enums.MensajeRespuesta onErrorMessage, string warningMessage = null, params string[] messageValues)
        {
            var response = new ResponseServices<bool>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            response.Info = checkBool;
            response.State = checkBool;
            response.Message = checkBool ? BetterMessage(onSuccessMessage, messageValues) : BetterMessage(onErrorMessage, messageValues);
            response.Warning = warningMessage ?? string.Empty;
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response to the api
        /// </summary>
        /// <param name="checkObject">List object to return to the api</param>
        /// <param name="onSuccessMessage">Success message</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkObject"/> null or empty</param>
        /// <param name="warningMessage"></param>
        /// <param name="messageValues">Array of values to replace on success message</param>
        /// <typeparam name="T">Type of list of objects to return.</typeparam>
        /// <returns>ResponseServices of type IEnumerable type T</returns>
        public static ResponseServices<IEnumerable<T>> GenericResponseArray<T>(IEnumerable<T> checkObject,
            Enums.MensajeRespuesta onSuccessMessage, Enums.MensajeRespuesta onErrorMessage,
            string warningMessage = null,
            params string[] messageValues)
        {
            var response = new ResponseServices<IEnumerable<T>>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            var success = checkObject != null && checkObject.Any();

            response.Info = checkObject;
            response.State = success;
            response.Message = success ? BetterMessage(onSuccessMessage, messageValues) : BetterMessage(onErrorMessage, messageValues);
            response.Warning = warningMessage ?? string.Empty;
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Generic method that gives a generic response from primitive to the api
        /// </summary>
        /// <param name="checkPrimitive">Primivite variable to return to the api</param>
        /// <param name="onSuccessMessage">Success message</param>
        /// <param name="onErrorMessage">Error message on <paramref name="checkPrimitive"/> value 0</param>
        /// <param name="warningMessage"></param>
        /// <param name="messageValues">Array of values to replace on success message</param>
        /// <typeparam name="T">Type of primitive object to return.</typeparam>
        /// <returns>ResponseServices of type primitive</returns>
        public static ResponseServices<T> GenericResponsePrimitive<T>(T checkPrimitive,
            Enums.MensajeRespuesta onSuccessMessage, Enums.MensajeRespuesta onErrorMessage,
            string warningMessage = null,
            params string[] messageValues) where T : struct
        {
            var response = new ResponseServices<T>
            {
                Type = Enums.MensajeRespuesta.Consulta.ToStringAttribute()
            };

            var success = !(checkPrimitive.Equals(default(int)) || checkPrimitive.Equals(default(float)) ||
                            checkPrimitive.Equals(default(long)));

            response.Info = checkPrimitive;
            response.State = success;
            var primitiveArray = new[] {checkPrimitive.ToString()};
            response.Message = success ? BetterMessage(onSuccessMessage, primitiveArray.Concat(messageValues).ToArray()) : BetterMessage(onErrorMessage, messageValues);
            response.Warning = warningMessage ?? string.Empty;
            response.TransactionId = TransversalHelpers.Instance.TransactionId;
            return response;
        }

        /// <summary>
        /// Replace content of ResponseMessage enum with real values and return that message to the user.
        /// </summary>
        /// <param name="message">Message from ResponseMessage enum to replace values for.</param>
        /// <param name="messageValues">Messages to replace on <paramref name="message"/></param>
        /// <returns>New and better message.</returns>
        private static string BetterMessage(Enums.MensajeRespuesta message, params string[] messageValues)
        {
            var messageString = message.ToStringAttribute();
            for (var index = 0; index < messageValues.Length; index ++)
                messageString = messageString.Replace($":{index}", messageValues[index]);

            return messageString;
        }
    }
}
