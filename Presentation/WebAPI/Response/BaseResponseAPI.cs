using System.Runtime.Serialization;
using AikoLearning.Core.Application.Categories.Commands;

namespace AikoLearning.Presentation.WebAPI.Response;

[DataContract]
public class BaseResponseAPI
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "data")]
    public object Data { get; set; }

    private BaseResponseAPI() { }

    public static BaseResponseAPI Create<T>(object obj, bool success = true)
    {
        var response = new BaseResponseAPI
        { 
            Success = success,
            Data = obj
        };

        return response;
    }
}