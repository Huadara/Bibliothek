<template>
  <div v-bind:style="{ color: 'white' }">
    <h1>Store Book</h1>
    <b-container>
      <b-row>
        <b-col>
          <div>
            <h4>Book</h4>
            <input class="form-control" placeholder="Search Book Title" v-model="searchBookFilter" @keyup="filterBooks">
          </div>
        </b-col>
        <b-col>
          <b-dropdown v-bind:text="get_name_book(currentBook)" id="whitespace">
            <b-dropdown-item v-for="book in filteredBooks" :key="book.id"
                             @click="selectedBookChanged(book)">
              {{get_name_book(book)}}
            </b-dropdown-item>
          </b-dropdown>
        </b-col>
      </b-row>
      <b-row class="p-3">
        <b-col>
          <h4>Store:</h4>
        </b-col>
        <b-col>
          <h5>{{currentStore}}</h5>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Amount:</h4>
        </b-col>
        <b-col>
          <input v-model="amount">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="storeBook">Submit</b-button>
        </b-col>
      </b-row>
    </b-container>
    <span>{{msg}}</span>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'StoreBookComponent',
  data () {
    return {
      searchBookFilter: '',
      currentBook: Object,
      currentStore: '',
      amount: '',
      books: [],
      filteredBooks: [],
      action: '',
      msg: ''
    }
  },
  methods: {
    // books
    doResponseBooks (response) {
      this.books = response
      this.filteredBooks = this.books
    },
    filterBooks () {
      this.filteredBooks = this.books
        .filter(book => book.title.toLowerCase().startsWith(this.searchBookFilter.toLowerCase()))
    },
    selectedBookChanged (book) {
      this.currentBook = book
      const self = this
      const url = 'http://localhost:5000/library/books/GetStoredBook/' +
          this.currentBook.book_id + '/' +
          this.currentStore
      axios.get(url)
        .then(function (response) {
          if (response != null) console.log('response')
          self.msg = response.data.message
        }).catch(function (error) {
          if (error != null) console.log('error')
          self.msg = 'Please check your entries'
        })
    },
    get_name_book (book) {
      if (book.title === undefined) return 'Select Book'
      return book.title
    },
    // store
    storeBook () {
      const self = this
      axios.post('http://localhost:5000/library/books/StoreBook', {
        book_id: this.currentBook.book_id,
        store_id: this.currentStore,
        amount: this.amount
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = response.data.message
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
      self.book_id = ''
      self.store_id = ''
      self.amount = ''
    }
  },
  props: {
    serverBooks: ''
  },
  watch: {
    serverBooks: function (data) {
      const self = this
      self.doResponseBooks(data)
      self.currentStore = self.$parent.$parent.$el.getElementsByTagName('span')[0].textContent
    }
  }
}
</script>

<style scoped>
  #whitespace {
    padding-top: 2.6em;
  }
</style>
