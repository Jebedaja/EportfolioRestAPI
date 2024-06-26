<template>
  <!-- Główny kontener dla widoku portfolio -->
  <div class="portfolio-container">
    <!-- Pudełko zawierające zawartość portfolio -->
    <div class="portfolio-box">
      <!-- Nagłówek sekcji portfolio -->
      <h2>Portfolio</h2>
      <!-- Wyświetlanie nazwy zalogowanego użytkownika -->
      <div v-if="username" class="user-info">
        <p>Zalogowany jako: {{ username }}</p>
      </div>
      <!-- Przycisk dodawania nowego portfolio -->
      <div class="add-button-container">
        <router-link :to="{ name: 'add-portfolio' }">
          <button class="add-button">Add New Portfolio</button>
        </router-link>
      </div>
      <!-- Pole wyszukiwania portfolio po nazwie -->
      <div class="search-container">
        <input type="text" v-model="searchQuery" placeholder="Search by portfolio name" class="search-input">
      </div>
      <!-- Lista przefiltrowanych portfolio -->
      <div v-if="filteredPortfolios.length > 0">
        <!-- Iteracja przez listę portfolio -->
        <div v-for="portfolio in filteredPortfolios" :key="portfolio.id" class="portfolio-item">
          <h3>{{ portfolio.portfolioName }}</h3>
          <p>{{ portfolio.description }}</p>
          <!-- Osadzony film YouTube, jeśli link jest poprawny -->
          <iframe
            v-if="isYouTubeLink(portfolio.youTubeLink)"
            :src="getYouTubeEmbedUrl(portfolio.youTubeLink)"
            frameborder="0"
            allowfullscreen
          ></iframe>
          <!-- Przyciski edytowania i usuwania portfolio -->
          <div class="button-container">
            <button @click="deletePortfolio(portfolio.id)" class="delete-button">Delete</button>
            <router-link :to="{ name: 'edit-portfolio', params: { id: portfolio.id }}" class="edit-button">Edit</router-link>
          </div>
        </div>
      </div>
      <!-- Komunikat wyświetlany, gdy brak portfolio -->
      <div v-else>
        <p>No portfolio content available.</p>
      </div>
      <!-- Komunikat o błędzie -->
      <div v-if="error" class="error-message">
        <p>{{ error }}</p>
      </div>
    </div>
  </div>
</template>

<script>
// Importowanie jQuery
import $ from 'jquery';
// Importowanie biblioteki do dekodowania JWT
import jwt_decode from 'jwt-decode';

export default {
  // Dane komponentu
  data() {
    return {
      portfolios: [], // Lista portfolio
      error: null, // Zmienna przechowująca błędy
      username: null, // Nazwa użytkownika
      searchQuery: '' // Zapytanie wyszukiwania
    };
  },
  // Akcje do wykonania po zamontowaniu komponentu
  mounted() {
    this.fetchPortfolioContent(); // Pobiera zawartość portfolio
    this.setUsername(); // Ustawia nazwę użytkownika
  },
  // Właściwości obliczane
  computed: {
    // Filtrowanie portfolio na podstawie zapytania wyszukiwania
    filteredPortfolios() {
      return this.portfolios.filter(portfolio =>
        portfolio.portfolioName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  },
  // Metody komponentu
  methods: {
    // Pobiera zawartość portfolio z serwera
    fetchPortfolioContent() {
      const token = localStorage.getItem('token'); // Pobranie tokenu z localStorage
      $.ajax({
        url: '/api/Account/Portfolio', // Endpoint API do pobierania portfolio
        type: 'GET', // Metoda HTTP
        headers: {
          Authorization: `Bearer ${token}` // Nagłówek autoryzacji
        },
        success: (data) => {
          this.portfolios = data; // Przypisanie pobranych danych do listy portfolio
        },
        error: (jqXHR, textStatus, errorThrown) => {
          this.error = `Failed to fetch portfolio content: ${jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown}`; // Ustawienie komunikatu o błędzie
          console.error('Failed to fetch portfolio content', jqXHR); // Logowanie błędu
        }
      });
    },
    // Usuwa portfolio na podstawie jego ID
    deletePortfolio(id) {
      const token = localStorage.getItem('token'); // Pobranie tokenu z localStorage
      $.ajax({
        url: `/api/Account/Portfolio/${id}`, // Endpoint API do usuwania portfolio
        type: 'DELETE', // Metoda HTTP
        headers: {
          Authorization: `Bearer ${token}` // Nagłówek autoryzacji
        },
        success: () => {
          this.portfolios = this.portfolios.filter(p => p.id !== id); // Usunięcie portfolio z listy
        },
        error: (jqXHR, textStatus, errorThrown) => {
          this.error = `Failed to delete portfolio with id ${id}: ${jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown}`; // Ustawienie komunikatu o błędzie
          console.error(`Failed to delete portfolio with id ${id}`, jqXHR); // Logowanie błędu
        }
      });
    },
    // Ustawia nazwę użytkownika na podstawie tokenu JWT
    setUsername() {
      const token = localStorage.getItem('token'); // Pobranie tokenu z localStorage
      if (token) {
        const decodedToken = jwt_decode(token); // Dekodowanie tokenu JWT
        this.username = decodedToken.sub; // Zakładam, że nazwa użytkownika jest w klajmie "sub"
        console.log('Decoded token:', decodedToken); // Logowanie zawartości tokenu
      }
    },
    // Sprawdza, czy link jest linkiem do YouTube
    isYouTubeLink(link) {
      return link.includes('youtube.com') || link.includes('youtu.be'); // Zwraca true, jeśli link jest do YouTube
    },
    // Zwraca URL do osadzenia filmu z YouTube
    getYouTubeEmbedUrl(link) {
      let videoId = ''; // Inicjalizacja zmiennej na ID wideo
      if (link.includes('youtube.com')) {
        videoId = link.split('v=')[1]; // Pobranie ID wideo z URL
      } else if (link.includes('youtu.be')) {
        videoId = link.split('.be/')[1]; // Pobranie ID wideo z URL
      }
      const ampersandPosition = videoId.indexOf('&'); // Znalezienie pozycji '&' w ID wideo
      if (ampersandPosition !== -1) {
        videoId = videoId.substring(0, ampersandPosition); // Ucięcie ID wideo do '&'
      }
      return `https://www.youtube.com/embed/${videoId}`; // Zwrócenie URL do osadzenia w iframe
    }
  }
};
</script>

<style scoped>
/* Stylizowanie głównego kontenera */
.portfolio-container {
  display: flex; /* Ustawienie elastycznego kontenera */
  justify-content: center; /* Wyśrodkowanie zawartości poziomo */
  align-items: center; /* Wyśrodkowanie zawartości pionowo */
  min-height: 100vh; /* Minimalna wysokość kontenera */
  background-color: #e0e0e0; /* Ustawienie tła */
}

/* Stylizowanie pudełka z zawartością portfolio */
.portfolio-box {
  text-align: center; /* Wyśrodkowanie tekstu */
  padding: 20px; /* Dodanie wewnętrznego marginesu */
  background: white; /* Ustawienie tła */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Dodanie cienia */
  border-radius: 8px; /* Zaokrąglenie rogów */
  width: 80%; /* Ustawienie szerokości */
  max-width: 800px; /* Maksymalna szerokość */
}

/* Stylizowanie nagłówka */
h2 {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
  color: #333; /* Ustawienie koloru tekstu */
}

/* Stylizowanie informacji o użytkowniku */
.user-info {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
  color: #555; /* Ustawienie koloru tekstu */
}

/* Stylizowanie kontenera przycisku dodawania */
.add-button-container {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
}

/* Stylizowanie kontenera wyszukiwania */
.search-container {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
}

/* Stylizowanie pola wyszukiwania */
.search-input {
  width: calc(100% - 22px); /* Ustawienie szerokości pola */
  padding: 10px; /* Dodanie wewnętrznego marginesu */
  font-size: 16px; /* Ustawienie rozmiaru czcionki */
  border: 1px solid #ccc; /* Ustawienie koloru ramki */
  border-radius: 4px; /* Zaokrąglenie rogów */
}

/* Stylizowanie zawartości */
.content {
  text-align: left; /* Wyrównanie tekstu do lewej */
}

/* Stylizowanie elementu portfolio */
.portfolio-item {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
  padding: 10px; /* Dodanie wewnętrznego marginesu */
  border: 1px solid #ccc; /* Ustawienie koloru ramki */
  border-radius: 8px; /* Zaokrąglenie rogów */
}

/* Stylizowanie nagłówka elementu portfolio */
.portfolio-item h3 {
  margin-bottom: 10px; /* Dodanie dolnego marginesu */
  color: #555; /* Ustawienie koloru tekstu */
}

/* Stylizowanie opisu elementu portfolio */
.portfolio-item p {
  margin-bottom: 10px; /* Dodanie dolnego marginesu */
  color: #777; /* Ustawienie koloru tekstu */
}

/* Stylizowanie iframe */
.portfolio-item iframe {
  width: 100%; /* Ustawienie szerokości iframe */
  height: 315px; /* Ustawienie wysokości iframe */
  margin-bottom: 10px; /* Dodanie dolnego marginesu */
}

/* Stylizowanie przycisków */
.add-button,
.delete-button,
.edit-button {
  background-color: #b6b6b6; /* Ustawienie koloru tła */
  color: white; /* Ustawienie koloru tekstu */
  border: none; /* Usunięcie ramki */
  padding: 8px 16px; /* Dodanie wewnętrznego marginesu */
  border-radius: 4px; /* Zaokrąglenie rogów */
  margin-top: 10px; /* Dodanie górnego marginesu */
  cursor: pointer; /* Ustawienie kursora */
}

/* Dodanie dolnego marginesu dla przycisku dodawania */
.add-button {
  margin-bottom: 10px;
}

/* Stylizowanie przycisków podczas najechania */
.add-button:hover,
.delete-button:hover,
.edit-button:hover {
  background-color: #3b3c3c; /* Zmiana koloru tła */
}

/* Stylizowanie przycisku edytowania */
.edit-button {
  color: white; /* Ustawienie koloru tekstu */
}

/* Stylizowanie kontenera przycisków */
.button-container {
  display: flex; /* Ustawienie elastycznego kontenera */
  justify-content: center; /* Wyśrodkowanie zawartości */
  gap: 10px; /* Dodanie odstępu między przyciskami */
}

/* Stylizowanie komunikatu o błędzie */
.error-message {
  color: red; /* Ustawienie koloru tekstu */
  margin-top: 20px; /* Dodanie górnego marginesu */
}
</style>
