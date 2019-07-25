import { Gulp } from "gulp";
const { exec } = require('child_process');
var runSequence = require('run-sequence');

var gulp: Gulp = require("gulp");
var isDebug = true;

gulp.task('build-angular', function (cb) {
    exec('ng build --prod=' + !isDebug + ' --baseHref=/', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});

gulp.task('copy-contents', function (cb) {
    gulp.src("dist/angular-app/**/*")
        .pipe(gulp.dest("../JMovies.Web.UI/App/"));
});

gulp.task('package', function (cb) {
    runSequence('build-angular', 'copy-contents', function () {
        cb();
    });
});
gulp.task('release-package', function (cb) {
    isDebug = false;
    runSequence('build-angular', 'copy-contents', function () {
        cb();
    });
});
gulp.task('run-servers', function (cb) {
    exec('cd ../SolutionItems/BatchFiles && run-servers.bat', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});