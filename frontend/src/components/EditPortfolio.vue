<template>
  <div class="edit-portfolio">
    <h2>Edit Portfolio</h2>
    <form @submit.prevent="updatePortfolio">
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
      <button type="submit">Update Portfolio</button>
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      portfolio: {
        id: null,
        portfolioName: '',
        description: '',
        youTubeLink: ''
      },
      errorMessage: '',
      portfolioNameError: '',
      descriptionError: '',
      youTubeLinkError: ''
    };
  },
  async created() {
    const id = this.$route.params.id;
    await this.fetchPortfolio(id);
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
    async fetchPortfolio(id) {
      try {
        const response = await axios.get(`/api/Account/Portfolio/${id}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        this.portfolio = response.data;
      } catch (error) {
        console.error(`Failed to fetch portfolio with id ${id}`, error);
        this.errorMessage = `Failed to fetch portfolio with id ${id}: ${error.response ? error.response.data : error.message}`;
      }
    },
    async updatePortfolio() {
      this.validatePortfolioName();
      this.validateDescription();
      this.validateYouTubeLink();

      if (this.portfolioNameError || this.descriptionError || this.youTubeLinkError) {
        return;
      }

      try {
        await axios.put(`/api/Account/Portfolio/${this.portfolio.id}`, this.portfolio, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        this.$router.push({ name: 'portfolio' });
      } catch (error) {
        console.error(`Failed to update portfolio with id ${this.portfolio.id}`, error);
        this.errorMessage = `Failed to update portfolio with id ${this.portfolio.id}: ${error.response ? error.response.data : error.message}`;
      }
    }
  }
};
</script>

<style scoped>
.edit-portfolio {
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
