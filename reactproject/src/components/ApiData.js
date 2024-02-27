import axios from 'axios';

const API_BASE_URL = 'https://localhost:7033/api/cards' // Base URL for the API

// Get all cards
export const getCards = async () => {
    try {
        const response = await axios.get(API_BASE_URL);
        return response.data;
    } catch (error) {
        console.error('Error fetching cards:', error);
        throw error;
    }
};

// Get a card by its ID
export const getCardById = async (id) => {
    try {
        const response = await axios.get(`${API_BASE_URL}/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching card by id:', error);
        throw error;
    }
};

// Create a new card
export const createCard = async (card) => {
    try {
        const response = await axios.post(API_BASE_URL, card);
        return response.data;
    } catch (error) {
        console.error('Error creating card:', error);
        throw error;
    }
};

// Update an existing card
export const updateCard = async (id, card) => {
    try {
        const response = await axios.put(`${API_BASE_URL}/${id}`, card);
        return response.data;
    } catch (error) {
        console.error('Error updating card:', error);
        throw error;
    }
};

// Delete a card
export const deleteCard = async (id) => {
    try {
        await axios.delete(`${API_BASE_URL}/${id}`);
    } catch (error) {
        console.error('Error deleting card:', error);
        throw error;
    }
};
