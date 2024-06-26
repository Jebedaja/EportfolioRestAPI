<template>
  <!-- Główny kontener dla widoku edycji portfolio -->
  <div class="edit-portfolio">
    <!-- Nagłówek sekcji edycji portfolio -->
    <h2>Edit Portfolio</h2>
    <!-- Formularz edycji portfolio -->
    <form @submit.prevent="updatePortfolio">
      <!-- Pole do edycji nazwy portfolio -->
      <div class="form-group">
        <label for="portfolioName">Portfolio Name:</label>
        <input type="text" id="portfolioName" v-model="portfolio.portfolioName" @input="validatePortfolioName" required>
        <div v-if="portfolioNameError" class="error-message">{{ portfolioNameError }}</div>
      </div>
      <!-- Pole do edycji opisu portfolio -->
      <div class="form-group">
        <label for="description">Description:</label>
        <textarea id="description" v-model="portfolio.description" @input="validateDescription" required></textarea>
        <div v-if="descriptionError" class="error-message">{{ descriptionError }}</div>
      </div>
      <!-- Pole do edycji linku YouTube -->
      <div class="form-group">
        <label for="youTubeLink">YouTube Link:</label>
        <input type="text" id="youTubeLink" v-model="portfolio.youTubeLink" @input="validateYouTubeLink" required>
        <div v-if="youTubeLinkError" class="error-message">{{ youTubeLinkError }}</div>
      </div>
      <!-- Przycisk do zatwierdzenia zmian -->
      <button type="submit">Update Portfolio</button>
      <!-- Komunikat o błędzie -->
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
// Importowanie biblioteki axios do obsługi zapytań HTTP
import axios from 'axios';

export default {
  // Dane komponentu
  data() {
    return {
      portfolio: {
        id: null,
        portfolioName: '',
        description: '',
        youTubeLink: ''
      },
      errorMessage: '', // Zmienna przechowująca ogólny komunikat o błędzie
      portfolioNameError: '', // Zmienna przechowująca błąd nazwy portfolio
      descriptionError: '', // Zmienna przechowująca błąd opisu
      youTubeLinkError: '' // Zmienna przechowująca błąd linku YouTube
    };
  },
  // Akcje do wykonania po utworzeniu komponentu
  async created() {
    const id = this.$route.params.id; // Pobranie ID portfolio z parametrów trasy
    await this.fetchPortfolio(id); // Pobranie danych portfolio
  },
  // Metody komponentu
  methods: {
    // Walidacja nazwy portfolio
    validatePortfolioName() {
      if (this.portfolio.portfolioName.length > 50) {
        this.portfolioNameError = 'Portfolio Name must be 50 characters or less.';
      } else {
        this.portfolioNameError = '';
      }
    },
    // Walidacja opisu portfolio
    validateDescription() {
      if (this.portfolio.description.length > 255) {
        this.descriptionError = 'Description must be 255 characters or less.';
      } else {
        this.descriptionError = '';
      }
    },
    // Walidacja linku YouTube
    validateYouTubeLink() {
      if (this.portfolio.youTubeLink.length > 255) {
        this.youTubeLinkError = 'YouTube Link must be 255 characters or less.';
      } else {
        this.youTubeLinkError = '';
      }
    },
    // Pobranie danych portfolio z serwera
    async fetchPortfolio(id) {
      try {
        const response = await axios.get(`/api/Account/Portfolio/${id}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        this.portfolio = response.data; // Przypisanie pobranych danych do obiektu portfolio
      } catch (error) {
        console.error(`Failed to fetch portfolio with id ${id}`, error);
        this.errorMessage = `Failed to fetch portfolio with id ${id}: ${error.response ? error.response.data : error.message}`;
      }
    },
    // Aktualizacja danych portfolio
    async updatePortfolio() {
      this.validatePortfolioName(); // Walidacja nazwy portfolio
      this.validateDescription(); // Walidacja opisu portfolio
      this.validateYouTubeLink(); // Walidacja linku YouTube

      // Sprawdzenie, czy są jakiekolwiek błędy walidacji
      if (this.portfolioNameError || this.descriptionError || this.youTubeLinkError) {
        return; // Jeśli są błędy, przerwanie aktualizacji
      }

      try {
        await axios.put(`/api/Account/Portfolio/${this.portfolio.id}`, this.portfolio, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        this.$router.push({ name: 'portfolio' }); // Przekierowanie do strony z listą portfolio po pomyślnej aktualizacji
      } catch (error) {
        console.error(`Failed to update portfolio with id ${this.portfolio.id}`, error);
        this.errorMessage = `Failed to update portfolio with id ${this.portfolio.id}: ${error.response ? error.response.data : error.message}`;
      }
    }
  }
};
</script>

<style scoped>
/* Stylizowanie głównego kontenera */
.edit-portfolio {
  text-align: center; /* Wyśrodkowanie tekstu */
  padding: 20px; /* Dodanie wewnętrznego marginesu */
  background: white; /* Ustawienie tła */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Dodanie cienia */
  border-radius: 8px; /* Zaokrąglenie rogów */
  width: 80%; /* Ustawienie szerokości */
  max-width: 800px; /* Maksymalna szerokość */
  margin: auto; /* Wyśrodkowanie kontenera */
}

/* Stylizowanie nagłówka */
h2 {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
  color: #333; /* Ustawienie koloru tekstu */
}

/* Stylizowanie grupy formularza */
.form-group {
  margin-bottom: 20px; /* Dodanie dolnego marginesu */
}

/* Stylizowanie etykiety */
label {
  display: block; /* Wyświetlenie jako blok */
  margin-bottom: 8px; /* Dodanie dolnego marginesu */
  color: #555; /* Ustawienie koloru tekstu */
}

/* Stylizowanie pola tekstowego i tekstarea */
input[type="text"],
textarea {
  width: 100%; /* Ustawienie szerokości */
  padding: 8px; /* Dodanie wewnętrznego marginesu */
  font-size: 16px; /* Ustawienie rozmiaru czcionki */
  border: 1px solid #ccc; /* Ustawienie koloru ramki */
  border-radius: 4px; /* Zaokrąglenie rogów */
}

/* Stylizowanie przycisku */
button {
  background-color: #b6b6b6; /* Ustawienie koloru tła */
  color: white; /* Ustawienie koloru tekstu */
  border: none; /* Usunięcie ramki */
  padding: 10px 20px; /* Dodanie wewnętrznego marginesu */
  border-radius: 4px; /* Zaokrąglenie rogów */
  cursor: pointer; /* Ustawienie kursora */
}

/* Stylizowanie przycisku podczas najechania */
button:hover {
  background-color: #3b3c3c; /* Zmiana koloru tła */
}

/* Stylizowanie komunikatu o błędzie */
.error-message {
  margin-top: 20px; /* Dodanie górnego marginesu */
  color: red; /* Ustawienie koloru tekstu */
}
</style>
