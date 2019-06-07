<template>
  <div>
    <h1>SUPPLIER</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Company Name:</h4>
        </b-col>
        <b-col>
          <input v-model="companyName">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Address:</h4>
        </b-col>
        <b-col>
          <input v-model="address">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="addSupplier">Add Supplier</b-button>
        </b-col>
      </b-row>
    </b-container>
    <h1>{{addSupplier(companyName, address)}}</h1>
  </div>
</template>

<script>

  export default {
    name: "AddSupplierComponent",
    data() {
      return {
        companyName: "",
        address: ""
      }
    },
    methods: {
      addSupplier(company_name, address) {

        const json = `{ "company_name": "${company_name}", "address": "${address}" }`;
        return json;
      }
    }
  }
</script>

<style scoped>

</style>
