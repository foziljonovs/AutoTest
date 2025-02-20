using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Question;
using AutoTest.Desktop.Integrated.Servers.Repositories.Question;

namespace AutoTest.Desktop.Integrated.Services.Question;

public class QuestionService : IQuestionService
{
    private readonly IQuestionServer _server;
    public QuestionService()
    {
        this._server = new QuestionServer();
    }
    public async Task<long> AddAsync(CreateQuestionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var result = await _server.AddAsync(dto);
                return result;
            }
            else
            {
                return -1;
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var result = await _server.DeleteAsync(id);
                return result;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<QuestionDto>> GetAllAsync()
    {
        try
        {
            if (IsInternetAvailable())
            {
                var result = await _server.GetAllAsync();
                return result;
            }
            else
            {
                return new List<QuestionDto>();
            }
        }
        catch (Exception ex)
        {
            return new List<QuestionDto>();
        }
    }

    public async Task<List<QuestionDto>> GetQuestionsByTestAsync(long testId)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var questions = await _server.GetQuestionsByTestAsync(testId);
                return questions;
            }
            else
            {
                return new List<QuestionDto>();
            }
        }
        catch (Exception ex)
        {
            return new List<QuestionDto>();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateQuestionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
            {
                var result = await _server.UpdateAsync(id, dto);
                return result;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool IsInternetAvailable()
    {
        try
        {
            return true;
            //using(Ping ping = new Ping())
            //{
            //    PingReply reply = ping.Send("www.google.com");
            //    return (reply.Status == IPStatus.Success);
            //}
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
