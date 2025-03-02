using AutoTest.BLL.DTOs.Tests.QuestionSolution;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Question;
using AutoTest.Desktop.Integrated.Servers.Repositories.Question;

namespace AutoTest.Desktop.Integrated.Services.Question;

public class QuestionSolutionService : IQuestionSolutionService
{
    private readonly IQuestionSolutionServer _server;
    public QuestionSolutionService()
    {
        this._server = new QuestionSolutionServer();
    }
    public async Task<bool> AddAsync(CreateQuestionSolutionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.AddAsync(dto);
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            if(IsInternetAvailable())
                return await _server.DeleteAsync(id);
            else
                return false;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<List<QuestionSolutionDto>> GetAllAsync()
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetAllAsync();
            else
                return new List<QuestionSolutionDto>();
        }
        catch (Exception ex)
        {
            return new List<QuestionSolutionDto>();
        }
    }

    public async Task<List<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetAllByTestSolutionIdAsync(testSolutionId);
            else
                return new List<QuestionSolutionDto>();
        }
        catch(Exception ex)
        {
            return new List<QuestionSolutionDto>();
        }
    }

    public async Task<QuestionSolutionDto> GetByIdAsync(long id)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.GetByIdAsync(id);
            else
                return new QuestionSolutionDto();
        }
        catch (Exception ex)
        {
            return new QuestionSolutionDto();
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto)
    {
        try
        {
            if (IsInternetAvailable())
                return await _server.UpdateAsync(id, dto);
            else
                return false;
        }
        catch(Exception ex)
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
