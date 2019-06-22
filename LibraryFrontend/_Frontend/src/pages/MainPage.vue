<template>
    <!-- v-bind:style="{ backgroundImage: 'url(' + '../images/library.png' + ')' }" -->
    <div v-bind:style="backgroundStyle">
        <!--reference to store number: this.$parent.$parent.$el.getElementsByTagName('span')[0].textContent-->
        <section v-bind:style="headerStyle">
            <p align="left" v-bind:style="{ color: 'white' }">Store <span>4</span> </p>
            <span v-bind:style="titleStyle">Bibliothek</span>
        </section>
        <section>
            <div>
                <b-button v-b-toggle.collapse-1 size="lg" v-bind:style="actionButtonStyle">Add Customer</b-button>
                <b-collapse id="collapse-1" class="p-2" v-bind:style="actionStyle">
                    <addcustomer-component></addcustomer-component>
                </b-collapse>
            </div>
            <div>
                <b-button v-b-toggle.collapse-2 size="lg" v-bind:style="actionButtonStyle">Add Supplier</b-button>
                <b-collapse id="collapse-2" class="p-2" v-bind:style="actionStyle">
                    <addsupplier-component></addsupplier-component>
                </b-collapse>
            </div>
            <div>
                <b-button v-b-toggle.collapse-3 size="lg" v-bind:style="actionButtonStyle">Add Book</b-button>
                <b-collapse id="collapse-3" class="p-2" v-bind:style="actionStyle">
                    <addbook-component></addbook-component>
                </b-collapse>
            </div>
            <div>
                <b-button v-b-toggle.collapse-4 size="lg" v-bind:style="actionButtonStyle">Store Book</b-button>
                <b-collapse id="collapse-4" class="p-2" v-bind:style="actionStyle">
                    <storebook-component :server-books="serverBooks"></storebook-component>
                </b-collapse>
            </div>
            <div>
                <b-button v-b-toggle.collapse-5 size="lg" v-bind:style="actionButtonStyle">Lending / Purchase / Restore</b-button>
                <b-collapse id="collapse-5" class="p-2" v-bind:style="actionStyle">
                    <lending-component :server-customers="serverCustomers" :server-books="serverBooks"></lending-component>
                </b-collapse>
            </div>
            <div>
                <b-button v-b-toggle.collapse-6 size="lg" v-bind:style="actionButtonStyle">Show Lendings</b-button>
                <b-collapse id="collapse-6" class="p-2" v-bind:style="actionStyle">
                    <showlendings-component :lendings="lendings"></showlendings-component>
                </b-collapse>
            </div>
        </section>
    </div>
</template>

<script>
import AddCustomerComponent from '../components/AddCustomerComponent'
import AddBookComponent from '../components/AddBookComponent'
import AddSupplierComponent from '../components/AddSupplierComponent'
import StoreBookComponent from '../components/StoreBookComponent'
import LendingComponent from '../components/LendingComponent'
import ShowLendingsComponent from '../components/ShowLendingsComponent'
import TitleBooksImage from '../images/titlebooks.jpg'
import BookShelfImage from '../images/bookshelf.jpg'
// import BooksImage from '../images/books.jpg'

export default {
  name: 'MainPage',
  created () {
    const self = this
    self.$axios
      .get('http://localhost:5000/library/customers')
      .then(function (customerResponse) {
        self.serverCustomers = customerResponse.data
        self.$axios
          .get('http://localhost:5000/library/books')
          .then(function (bookResponse) {
            self.serverBooks = bookResponse.data
            self.$axios
              .get('http://localhost:5000/library/books/GetRelevantLendings')
              .then(function (lendingResponse) {
                self.lendings = lendingResponse.data
              })
          })
      })
  },
  data () {
    return {
      serverCustomers: '',
      serverBooks: '',
      lendings: '',
      headerStyle: {
        'backgroundImage': 'url(' + TitleBooksImage + ')',
        'backgroundSize': 'contain'
      },
      titleStyle: {
        'fontSize': '60px',
        'color': 'white'
      },
      backgroundStyle: {
        'margin': 0,
        'backgroundColor': '#990000'
      },
      actionStyle: {
        // 'backgroundImage': 'url(' + BooksImage + ')',
        // 'backgroundSize': 'contain'
        'backgroundColor': '#753403'
      },
      actionButtonStyle: {
        'backgroundImage': 'url(' + BookShelfImage + ')',
        'backgroundSize': 'contain',
        'width': '100%'
      }
    }
  },
  components: {
    'addcustomer-component': AddCustomerComponent,
    'addbook-component': AddBookComponent,
    'addsupplier-component': AddSupplierComponent,
    'storebook-component': StoreBookComponent,
    'lending-component': LendingComponent,
    'showlendings-component': ShowLendingsComponent
  }
}
</script>

<style scoped>

</style>
