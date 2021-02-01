const recommendations = [
    {
        artist: '100Gecs',
        title: '1000Gecs',
        tags: ['hyperpop', 'loud'],
        releaseDate: '2020-10-01',
        location: 'USA',
        url: 'http://google.pl',
        imagePath: 'https://ecsmedia.pl/c/1000-gecs-b-iext56907495.jpg'
    },
    {
        artist: 'Radiohead',
        title: 'OK Computer',
        tags: ['alt rock', 'artpop'],
        releaseDate: '1997-06-16',
        location: 'UK',
        url: 'http://google.pl',
        imagePath: 'https://ecsmedia.pl/c/1000-gecs-b-iext56907495.jpg'
    }
];

export const getRecommendations = (description, page) => new Promise((resolve, reject) => resolve(recommendations));