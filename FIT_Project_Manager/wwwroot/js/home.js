let target_idx = 0;
let max_page_len = 0;
let prev_btn = null;
let next_btn = null;
let target_page_num = $('.target-page-number');
let max_page_num = $('.max-page-number');

function set_page_number(){
    target_page_num.text((target_idx+1));
    max_page_num.text(max_page_len);
}

function setup_event(tgt_idx){
    let page_components = $('.home-record-area');
    max_page_len = page_components.length;
    set_page_number();
    page_components.each(function(index) {
        if (index != tgt_idx) { $(this).hide(); }
    });
    prev_btn = $('.circle_btn_prev');
    prev_btn.click(function () {
        if (target_idx > 0) {
            $(page_components[target_idx]).hide();
            target_idx -= 1;
            $(page_components[target_idx]).show();
            set_page_number();
        }
    });
    next_btn = $('.circle_btn_next');
    next_btn.click(function () {
        if (target_idx < max_page_len-1) {
            $(page_components[target_idx]).hide();
            target_idx += 1;
            $(page_components[target_idx]).show();
            set_page_number();
        }
    });
}

setup_event(target_idx);