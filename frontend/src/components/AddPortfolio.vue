<template>
  <div class="add-portfolio">
    <h2>Add Portfolio</h2>
    <form @submit.prevent="addPortfolio">
      <div class="form-group">
        <label for="portfolioName">Portfolio Name:</label>
        <input type="text" id="portfolioName" v-model="portfolio.portfolioName" @input="validatePortfolioName" required>
        <div v-if="portfolioNameError" class="error-message">{{ portfolioNameError }}</div>
      </div>
      <div class="form-group">
        <label for="description">Description:</label>
        <textarea id="description" v-model="portfolio.description" @input="validateDescription" required></textarea>
        <div v-if="descriptionError" class="error-message">{{ descriptionError }}</div>
      </div>
      <div class="form-group">
        <label for="youTubeLink">YouTube Link:</label>
        <input type="text" id="youTubeLink" v-model="portfolio.youTubeLink" @input="validateYouTubeLink" required>
        <div v-if="youTubeLinkError" class="error-message">{{ youTubeLinkError }}</div>
      </div>
      <button type="submit">Add Portfolio</button>
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
import $ from 'jquery';

export default {
  data() {
    return {
      portfolio: {
        portfolioName: '',
        description: '',
        youTubeLink: '',
        dateAdded: new Date()
      },
      errorMessage: '',
      portfolioNameError: '',
      descriptionError: '',
      youTubeLinkError: ''
    };
  },
  methods: {
    validatePortfolioName() {
      if (this.portfolio.portfolioName.length > 50) {
        this.portfolioNameError = 'Portfolio Name must be 50 characters or less.';
      } else {
        this.portfolioNameError = '';
      }
    },
    validateDescription() {
      if (this.portfolio.description.length > 255) {
        this.descriptionError = 'Description must be 255 characters or less.';
      } else {
        this.descriptionError = '';
      }
    },
    validateYouTubeLink() {
      if (this.portfolio.youTubeLink.length > 255) {
        this.youTubeLinkError = 'YouTube Link must be 255 characters or less.';
      } else {
        this.youTubeLinkError = '';
      }
    },
    async addPortfolio() {
      this.validatePortfolioName();
      this.validateDescription();
      this.validateYouTubeLink();

      if (this.portfolioNameError || this.descriptionError || this.youTubeLinkError) {
        return;
      }

      const token = localStorage.getItem('token');
      if (!token) {
        this.errorMessage = 'User is not authenticated. Please log in.';
        return;
      }

      console.log('Adding portfolio:', this.portfolio); // Logowanie danych przed wysłaniem

      $.ajax({
        url: '/api/Account/Portfolio',
        type: 'POST',
        data: JSON.stringify(this.portfolio),
        contentType: 'application/json',
        headers: {
          Authorization: `Bearer ${token}`
        },
        success: (response) => {
          console.log('Response:', response);
          this.$router.push({ name: 'portfolio' });
        },
        error: (jqXHR, textStatus, errorThrown) => {
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
.add-portfolio {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  width: 80%;
  max-width: 800px;
  margin: auto;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  color: #555;
}

input[type="text"],
textarea {
  width: 100%;
  padding: 8px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button {
  background-color: #b6b6b6;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #3b3c3c;
}

.error-message {
  margin-top: 20px;
  color: red;
}
</style>
