<template>
  <!-- Główny kontener dla widoku dodawania portfolio -->
  <div class="add-portfolio">
    <!-- Nagłówek sekcji dodawania portfolio -->
    <h2>Add Portfolio</h2>
    <!-- Formularz dodawania portfolio -->
    <form @submit.prevent="addPortfolio">
      <!-- Pole do wpisania nazwy portfolio -->
      <div class="form-group">
        <label for="portfolioName">Portfolio Name:</label>
        <input type="text" id="portfolioName" v-model="portfolio.portfolioName" @input="validatePortfolioName" required>
        <!-- Wyświetlanie błędu walidacji nazwy portfolio -->
        <div v-if="portfolioNameError" class="error-message">{{ portfolioNameError }}</div>
      </div>
      <!-- Pole do wpisania opisu portfolio -->
      <div class="form-group">
        <label for="description">Description:</label>
        <textarea id="description" v-model="portfolio.description" @input="validateDescription" required></textarea>
        <!-- Wyświetlanie błędu walidacji opisu portfolio -->
        <div v-if="descriptionError" class="error-message">{{ descriptionError }}</div>
      </div>
      <!-- Pole do wpisania linku YouTube -->
      <div class="form-group">
        <label for="youTubeLink">YouTube Link:</label>
        <input type="text" id="youTubeLink" v-model="portfolio.youTubeLink" @input="validateYouTubeLink" required>
        <!-- Wyświetlanie błędu walidacji linku YouTube -->
        <div v-if="youTubeLinkError" class="error-message">{{ youTubeLinkError }}</div>
      </div>
      <!-- Przycisk do zatwierdzenia dodania portfolio -->
      <button type="submit">Add Portfolio</button>
      <!-- Wyświetlanie ogólnego komunikatu o błędzie -->
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
// Importowanie biblioteki jQuery do obsługi zapytań AJAX
import $ from 'jquery';

export default {
  // Dane komponentu
  data() {
    return {
      // Obiekt przechowujący dane portfolio
      portfolio: {
        portfolioName: '',
        description: '',
        youTubeLink: '',
        dateAdded: new Date() // Data dodania portfolio
      },
      errorMessage: '', // Zmienna przechowująca ogólny komunikat o błędzie
      portfolioNameError: '', // Zmienna przechowująca błąd nazwy portfolio
      descriptionError: '', // Zmienna przechowująca błąd opisu portfolio
      youTubeLinkError: '' // Zmienna przechowująca błąd linku YouTube
    };
  },
  methods: {
    // Metoda walidacji nazwy portfolio
    validatePortfolioName() {
      if (this.portfolio.portfolioName.length > 50) {
        this.portfolioNameError = 'Portfolio Name must be 50 characters or less.';
      } else {
        this.portfolioNameError = '';
      }
    },
    // Metoda walidacji opisu portfolio
    validateDescription() {
      if (this.portfolio.description.length > 255) {
        this.descriptionError = 'Description must be 255 characters or less.';
      } else {
        this.descriptionError = '';
      }
    },
    // Metoda walidacji linku YouTube
    validateYouTubeLink() {
      if (this.portfolio.youTubeLink.length > 255) {
        this.youTubeLinkError = 'YouTube Link must be 255 characters or less.';
      } else {
        this.youTubeLinkError = '';
      }
    },
    // Metoda dodawania portfolio
    async addPortfolio() {
      // Wywołanie metod walidacji dla każdego pola
      this.validatePortfolioName();
      this.validateDescription();
      this.validateYouTubeLink();

      // Sprawdzenie, czy są jakiekolwiek błędy walidacji
      if (this.portfolioNameError || this.descriptionError || this.youTubeLinkError) {
        return; // Jeśli są błędy, przerwanie dodawania
      }

      // Sprawdzenie, czy użytkownik jest zalogowany
      const token = localStorage.getItem('token');
      if (!token) {
        this.errorMessage = 'User is not authenticated. Please log in.';
        return;
      }

      // Logowanie danych przed wysłaniem
      console.log('Adding portfolio:', this.portfolio);

      // Wysłanie zapytania AJAX do serwera
      $.ajax({
        url: '/api/Account/Portfolio',
        type: 'POST',
        data: JSON.stringify(this.portfolio),
        contentType: 'application/json',
        headers: {
          Authorization: `Bearer ${token}`
        },
        success: (response) => {
          // Logowanie odpowiedzi serwera
          console.log('Response:', response);
          // Przekierowanie do widoku portfolio po pomyślnym dodaniu
          this.$router.push({ name: 'portfolio' });
        },
        error: (jqXHR, textStatus, errorThrown) => {
          // Logowanie szczegółów błędu
          console.error('Failed to add portfolio', jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown);
          this.errorMessage = `Failed to add portfolio: ${jqXHR.responseJSON ? jqXHR.responseJSON : errorThrown}`;
          if (jqXHR.responseJSON) {
            console.error('Error details:', jqXHR.responseJSON);
          }
        }
      });
    }
  }
};
</script>

<style scoped>
/* Stylizowanie głównego kontenera */
.add-portfolio {
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
