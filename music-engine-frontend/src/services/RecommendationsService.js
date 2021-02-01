
const API_URL = '/api'

export const getRecommendations = (description, page) => fetch(`${API_URL}/albums/bytags/${description}/${page + 1}`)
    .then(response => response.json());
