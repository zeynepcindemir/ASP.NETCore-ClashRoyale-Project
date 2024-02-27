using WebApiProject.Dto;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface ICardRepository
    {
        Task<Card> CreateCard(CreateCardDto card);
        Task DeleteCard(int Id);
        Task<IEnumerable<Card>> GetAllCards();
        Task<Card> GetCardById(int Id);
        Task UpdateCard(int Id, UpdateCardDto card);
    }
}