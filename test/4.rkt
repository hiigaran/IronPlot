#lang racket/base
(define (zero? n)
    (eq? 0 n))


(letrec ([is-even? (lambda (n)
                       (or (zero? n)
                           (is-odd? (- n 1))))]
           [is-odd? (lambda (n)
                      (and (not (zero? n))
                           (is-even? (- n 1))))])
    (is-odd? 11))