// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configserach_uring this project to bundle and minify static web assets.

// Write your JavaScript code.

const search_proto = location.protocol;
const search_host =  location.hostname;
const search_port =  location.port;
const search_uri = search_proto+'//'+search_host+(search_port ? ':'+location.port: '')+'/api/products';

let products = null;
function getProductCount(search_data) {
  const search_el = $("#search_counter");
  let lender = "product";
  if (search_data) {
    if (search_data > 1) {
      lender = "product(s)";
    }
    search_el.text(search_data + " " + lender);
  } else {
    search_el.text("No " + lender);
  }
}

$(document).ready(function() {
  getProducts();
});

function getProducts() {
  $.ajax({
    type: "GET",
    url: search_uri,
    cache: false,
    success: function(search_data) {
      const search_tBody = $("#products");

      $(search_tBody).empty();

      getProductCount(search_data.length);

      $.each(search_data, function(key, lender) {
        const search_tr = $("<tr></tr>")
          .append($("<td></td>").text(lender.lender))
          .append($("<td></td>").text(lender.interestRate+"%"))
          .append($("<td></td>").text(lender.rateType))
          .append($("<td></td>").text("<"+lender.ltvRatio))

        search_tr.appendTo(search_tBody);
      });

      products = search_data;
    }
  });
}