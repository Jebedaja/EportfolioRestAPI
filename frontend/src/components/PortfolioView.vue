<template>
  <div class="portfolio-container">
    <div class="portfolio-box">
      <h2>Portfolio</h2>
      <div v-if="username" class="user-info">
        <p>Zalogowany jako: {{ username }}</p>
      </div>
      <div class="add-button-container">
        <router-link :to="{ name: 'add-portfolio' }">
          <button class="add-button">Add New Portfolio</button>
        </router-link>
      </div>
      <div class="search-container">
        <input type="text" v-model="searchQuery" placeholder="Search by portfolio name" class="search-input">
      </div>
      <div v-if="filteredPortfolios.length > 0">
        <div v-for="portfolio in filteredPortfolios" :key="portfolio.id" class="portfolio-item">
          <h3>{{ portfolio.portfolioName }}</h3>
          <p>{{ portfolio.description }}</p>
          <iframe
            v-if="isYouTubeLink(portfolio.youTubeLink)"
            :src="getYouTubeEmbedUrl(portfolio.youTubeLink)"
            frameborder="0"
            allowfullscreen
          ></iframe>
          <div class="button-container">
            <button @click="deletePortfolio(portfolio.id)" class="delete-button">Delete</button>
            <router-link :to="{ name: 'edit-portfolio', params: { id: portfolio.id }}" class="edit-button">Edit</router-link>
          </div>
        </div>
      </div>
      <div v-else>
        <p>No portfolio content available.</p>
      </div>
      <div v-if="error" class="error-message">
        <p>{{ error }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery';
import jwt_decode from 'jwt-decode'; // Importowanie biblioteki do dekodowania JWT

export default {
  data() {
    return {
      portfolios: [],
      error: null,
      username: null, // Dodanie pola na nazwę użytkownika
      searchQuery: '' // Dodanie pola do wyszukiwania
    };
  },
  mounted() {
    this.fetchPortfolioContent();
    this.setUsername(); // Ustawienie nazwy użytkownika po zamontowaniu komponentu
  },
  computed: {
    filteredPortfolios() {
      return this.portfolios.filter(portfolio =>
        portfolio.portfolioName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  },
  methods: {
    fetchPortfolioContent() {
      const token = localStorage.getItem('token');
      $.ajax({
        url: '/api/Account/Portfolio',
        type: 'GET',
        headers: {
          Authorization: `Bearer ${token}`
        },
        success: (data) => {
          this.portfolios = data;
        },
        error: (jqXHR, textStatus, errorThrown) => {
          this.error = `Failed to fetch portfolio content: ${jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown}`;
          console.error('Failed to fetch portfolio content', jqXHR);
        }
      });
    },
    deletePortfolio(id) {
      const token = localStorage.getItem('token');
      $.ajax({
        url: `/api/Account/Portfolio/${id}`,
        type: 'DELETE',
        headers: {
          Authorization: `Bearer ${token}`
        },
        success: () => {
          this.portfolios = this.portfolios.filter(p => p.id !== id);
        },
        error: (jqXHR, textStatus, errorThrown) => {
          this.error = `Failed to delete portfolio with id ${id}: ${jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown}`;
          console.error(`Failed to delete portfolio with id ${id}`, jqXHR);
        }
      });
    },
    setUsername() {
      const token = localStorage.getItem('token');
      if (token) {
        const decodedToken = jwt_decode(token); // Dekodowanie tokenu JWT
        this.username = decodedToken.sub; // Zakładam, że nazwa użytkownika jest w klajmie "sub"
        console.log('Decoded token:', decodedToken); // Logowanie zawartości tokenu
      }
    },
    isYouTubeLink(link) {
      return link.includes('youtube.com') || link.includes('youtu.be');
    },
    getYouTubeEmbedUrl(link) {
      let videoId = '';
      if (link.includes('youtube.com')) {
        videoId = link.split('v=')[1];
      } else if (link.includes('youtu.be')) {
        videoId = link.split('.be/')[1];
      }
      const ampersandPosition = videoId.indexOf('&');
      if (ampersandPosition !== -1) {
        videoId = videoId.substring(0, ampersandPosition);
      }
      return `https://www.youtube.com/embed/${videoId}`;
    }
  }
};
</script>

<style scoped>
.portfolio-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh; /* Zmienione z height na min-height */
  background-color: #e0e0e0;
}

.portfolio-box {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  width: 80%;
  max-width: 800px;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.user-info {
  margin-bottom: 20px;
  color: #555;
}

.add-button-container {
  margin-bottom: 20px; /* Dodanie odstępu od dołu */
}

.search-container {
  margin-bottom: 20px; /* Dodanie odstępu od dołu */
}

.search-input {
  width: calc(100% - 22px);
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.content {
  text-align: left;
}

.portfolio-item {
  margin-bottom: 20px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 8px;
}

.portfolio-item h3 {
  margin-bottom: 10px;
  color: #555;
}

.portfolio-item p {
  margin-bottom: 10px;
  color: #777;
}

.portfolio-item iframe {
  width: 100%;
  height: 315px;
  margin-bottom: 10px;
}

.add-button,
.delete-button,
.edit-button {
  background-color: #b6b6b6;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  margin-top: 10px;
  cursor: pointer;
}

.add-button {
  margin-bottom: 10px; /* Dodanie odstępu od dołu */
}

.add-button:hover,
.delete-button:hover,
.edit-button:hover {
  background-color: #3b3c3c;
}

.edit-button {
  color: white; /* Zmieniony kolor tekstu na biały */
}

.button-container {
  display: flex;
  justify-content: center;
  gap: 10px; /* Dodanie odstępu między przyciskami */
}

.error-message {
  color: red;
  margin-top: 20px;
}
</style>

