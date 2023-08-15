const gulp = require('gulp');
const concat = require('gulp-concat');

const vendorStyles = [
    "node_modules/bootstrap/dist/css/bootstrap.min.css",
    "node_modules/famfamfam-flags/dist/sprite/famfamfam-flags.min.css"
];
const vendorScripts = [
    "node_modules/jquery/dist/jquery.min.js",
    "node_modules/jquery-validation/dist/jquery.validate.min.js",
    "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js",
    "node_modules/popper.js/dist/umd/popper.min.js",
    "node_modules/bootstrap/dist/js/bootstrap.min.js"
];

const copyImgs = [
    "node_modules/famfamfam-flags/dist/sprite/famfamfam-flags.png"
]

gulp.task('default', ['build-vendor']);

gulp.task('build-vendor', ['build-vendor-css', 'build-vendor-js', 'build-vendor-img']);

gulp.task('build-vendor-css', () => {
  return gulp.src(vendorStyles)
      .pipe(concat('vendor.css'))
      .pipe(gulp.dest('wwwroot'));
});

gulp.task('build-vendor-js', () => {
  return gulp.src(vendorScripts)
      .pipe(concat('vendor.js'))
      .pipe(gulp.dest('wwwroot'));
});

gulp.task('build-vendor-img', () => {
  return gulp.src(copyImgs)
      .pipe(concat('famfamfam-flags.png'))
      .pipe(gulp.dest('wwwroot'));
})