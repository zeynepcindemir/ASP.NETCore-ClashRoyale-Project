import { useState, useEffect } from 'react';
import { getCards } from './components/ApiData';


function App() {

    const [cards, setCards] = useState([]);

    useEffect(() => {
        async function fetchCards() {
            try {
                const fetchedCards = await getCards();
                setCards(fetchedCards);
            } catch (error) {
                console.error('Error fetching cards:', error);
            }
        }

        fetchCards();
    }, []);

    return (
        <div>
            <h1>Cards</h1>
            <ul>
                {cards.map(card => (
                    <li key={card.id}>
                        <img src={`images/${card.name}.png`} alt={card.name} />
                        <div>{card.name} - {card.description}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
