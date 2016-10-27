; Sprawozdanie:
; 1. opis problemu
; 2. istniej¹ce rozw. 1 str.
; 3. koncepcja 1-2 str.
; 4. problemy implementacyjne
; 5. aplikacja - wyniki i testowanie
; prezentacja 5-7 min (12 slajdów)
; moodle katalog projektowy Balickiego

;;; ***************************
;;; * DEFTEMPLATES & DEFFACTS *
;;; ***************************

(deftemplate UI-state
   (slot id (default-dynamic (gensym*)))
   (slot display)
   (slot relation-asserted (default none))
   (slot response (default none))
   (multislot valid-answers)
   (slot state (default middle)))
   
(deftemplate state-list
   (slot current)
   (multislot sequence))

(deftemplate choroba
   (slot nazwa)
   (slot prawdopodobienstwo (default 0.0)))
  
(deffacts startup
   (state-list))
   
;;;****************
;;;* STARTUP RULE *
;;;****************

(defrule system-banner ""

  =>
  
  (assert (UI-state (display WelcomeMessage)
                    (relation-asserted start)
                    (state initial)
                    (valid-answers))))

;;;***************
;;;* QUERY RULES *
;;;***************

(defrule determine-body-temperature ""

   (logical (start))

   =>

   (assert (UI-state (display HighTemperatureQuestion)
                     (relation-asserted high-temperature)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-sore-throat ""
 
   (logical (start))

    =>

   (assert (UI-state (display SoreThroatQuestion)
                     (relation-asserted sore-throat)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-oslabienie ""

	(logical(start))
	
	=> 
	
	(assert (UI-state	(display OslabienieQuestion)
						(relation-asserted oslabienie)
						(response No)
						(valid-answers No Yes))))


(defrule determine-poczucie-rozbicia ""

   (logical (start))

    =>

   (assert (UI-state (display PoczucieRozbiciaQuestion)
                     (relation-asserted poczucie-rozbicia)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-bol-glowy ""

   (logical (start))

    =>

   (assert (UI-state (display BolGlowyQuestion)
                     (relation-asserted bol-glowy)
                     (response No)
                     (valid-answers No Yes))))
   
(defrule determine-tonsils-swollen ""

   (logical (sore-throat Yes))

   =>

   (assert (UI-state (display SwollenTonsilsQuestion)
                     (relation-asserted swollen-tonsils)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-zaczerwienienie ""

(logical (sore-throat Yes))

   =>

   (assert (UI-state (display ZaczerwienienieQuestion)
                     (relation-asserted zaczerwienienie)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-runny-nose ""

   (logical (start))

   =>

   (assert (UI-state (display RunnyNoseQuestion)
                     (relation-asserted runny-nose)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-dlugotrwaly-katar ""

   (logical (runny-nose Yes))

   =>

   (assert (UI-state (display DlugotrwalyKatarQuestion)
                     (relation-asserted dlugotrwaly-katar)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-kichanie ""

   (logical (high-temperature No))

   =>

   (assert (UI-state (display KichanieQuestion)
                     (relation-asserted kichanie)
                     (response No)
                     (valid-answers No Yes))))


(defrule determine-cough ""

   (logical (start))

   =>

   (assert (UI-state (display CoughQuestion)
                     (relation-asserted cough-any)
                     (response No)
                     (valid-answers No Yes))))


(defrule determine-dry-cough ""

   (logical (cough-any Yes))

   =>

   (assert (UI-state (display DryCoughQuestion)
                     (relation-asserted cough-dry)
                     (response No)
                     (valid-answers No Yes))))


(defrule determine-swiszczacy-oddech ""

   (logical (cough-dry No))

   =>

   (assert (UI-state (display SwiszczacyOddechQuestion)
                     (relation-asserted swiszczacy-oddech)
                     (response No)
                     (valid-answers No Yes))))

(defrule determine-suchy-kaszel ""

	(logical (cough-dry Yes))
	
	=>
	
	(assert (UI-state	(display SuchyKaszelQuestion)
						(relation-asserted suchy-kaszel)
						(response No)
						(valid-answers No Yes))))
											 

(defrule determine-long-term-cough ""

   (logical (cough-dry Yes))

   =>

   (assert (UI-state (display LongTermCoughQuestion)
                     (relation-asserted cough-long-term)
                     (response No)
                     (valid-answers No Yes))))


(defrule determine-drapanie-w-gardle ""

	(logical (runny-nose Yes))
	
	=>
	
	(assert (UI-state	(display DrapanieWGardleQuestion)
						(relation-asserted drapanie-w-gardle)
						(response No)
						(valid-answers No Yes))))

(defrule determine-bole-kostno-stawowe ""

	(logical (start))
	
	=>
	
	(assert (UI-state	(display BoleKostnoStawoweQuestion)
						(relation-asserted bole-kostno-stawowe)
						(response No)
						(valid-answers No Yes))))

(defrule determine-chrypka ""

	(logical (cough-dry Yes))
	
	=>
	
	(assert (UI-state	(display ChrypkaQuestion)
						(relation-asserted chrypka)
						(response No)
						(valid-answers No Yes))))

(defrule determine-rozpulchnienie ""

	(logical (chrypka No))
	
	=>
	
	(assert (UI-state	(display RozpulchnienieQuestion)
						(relation-asserted rozpulchnienie)
						(response No)
						(valid-answers No Yes))))	
						
(defrule determine-dyskomfort-w-krtani ""

	(logical (start))
	
	=>
	
	(assert (UI-state	(display DyskomfortWKrtaniQuestion)
						(relation-asserted dyskomfort-w-krtani)
						(response No)
						(valid-answers No Yes))))		
						
(defrule determine-dreszcze ""

	(logical (start))
	
	=>
	
	(assert (UI-state	(display DreszczeQuestion)
						(relation-asserted dreszcze)
						(response No)
						(valid-answers No Yes))))	
						
(defrule determine-brak-apetytu ""

	(logical (cough-dry Yes))
	
	=>
	
	(assert (UI-state	(display BrakApetytuQuestion)
						(relation-asserted brak-apetytu)
						(response No)
						(valid-answers No Yes))))		


;;;****************
;;;* ILLNESSES RULES *
;;;****************

  ;; objawy a prawdopodobienstwa chorob

  (defrule nic ""

 ;  (declare (salience 2))

   (logical (high-temperature No) (sore-throat No) (runny-nose No) (dreszcze No) (bole-kostno-stawowe No) (bol-glowy No) (cough-any No))
   
   =>

   (assert (choroba (nazwa none)
                     (prawdopodobienstwo 75)))
   (assert (choroba (nazwa inne)
                     (prawdopodobienstwo 25))))

 (defrule mokry-kaszel ""

	(logical (cough-any Yes) (cough-dry No))

	=>

	(assert (choroba (nazwa zapalenieOskrzeli)
						(prawdopodobienstwo 42))))

 (defrule kaszel ""

	(logical (cough-long-term))

	=>

	(assert (choroba (nazwa zapalenie)
						(prawdopodobienstwo 42))))

 (defrule cough-dry ""

	(logical (cough-dry Yes))

	=>

	(assert (choroba (nazwa zapalenie)
						(prawdopodobienstwo 22))))

  (defrule goraczka ""

 ;  (declare (salience 2))

   (logical (high-temperature Yes))
   
   =>

   (assert (choroba (nazwa grypa)
                     (prawdopodobienstwo 21)))
	(assert (choroba (nazwa niezyt)
                     (prawdopodobienstwo 5)))
	(assert (choroba (nazwa katarSienny)
                     (prawdopodobienstwo 5)))
   (assert (choroba (nazwa angina)
                     (prawdopodobienstwo 19)))
	(assert (choroba (nazwa zapalenieOskrzeli)
                     (prawdopodobienstwo 21)))
					 
	(assert (choroba (nazwa inna)
                     (prawdopodobienstwo 10))))

(defrule migdalki ""

 ;  (declare (salience 2))

   (logical (swollen-tonsils Yes))
   
   =>

   (assert (choroba (nazwa inna)
                     (prawdopodobienstwo 10)))
   (assert (choroba (nazwa angina)
                     (prawdopodobienstwo 24))))

(defrule katar ""

 ;  (declare (salience 2))

   (logical (runny-nose Yes))
   
   =>

   (assert (choroba (nazwa inna)
                     (prawdopodobienstwo 10)))
   (assert (choroba (nazwa katarSienny)
                     (prawdopodobienstwo 23)))
   (assert (choroba (nazwa niezyt)
                     (prawdopodobienstwo 23))))

(defrule dlugKatar ""

 ;  (declare (salience 2))

   (logical (dlugotrwaly-katar Yes))
   
   =>

   (assert (choroba (nazwa katarSienny)
                     (prawdopodobienstwo 30))))

(defrule kichanie ""

   (logical (kichanie Yes))
   
   =>

   (assert (choroba (nazwa inna)
                     (prawdopodobienstwo 15)))
	(assert (choroba (nazwa katarSienny)
                     (prawdopodobienstwo 18)))
   (assert (choroba (nazwa niezyt)
                     (prawdopodobienstwo 18))))


(defrule poczucie-rozbicia ""

   (logical (poczucie-rozbicia Yes))
   
   =>

   (assert (choroba (nazwa inna)
                     (prawdopodobienstwo 15)))
   (assert (choroba (nazwa angina)
                     (prawdopodobienstwo 5)))
   (assert (choroba (nazwa grypa)
                     (prawdopodobienstwo 13)))
   (assert (choroba (nazwa zapalenieOskrzeli)
                     (prawdopodobienstwo 7))))

(defrule bol-glowy ""

   (logical (bol-glowy Yes))
   
   =>

   (assert (choroba (nazwa inna)
                     (prawdopodobienstwo 8)))
	(assert (choroba (nazwa katarSienny)
                     (prawdopodobienstwo 9)))
   (assert (choroba (nazwa niezyt)
                     (prawdopodobienstwo 9)))
   (assert (choroba (nazwa grypa)
                     (prawdopodobienstwo 17)))
   (assert (choroba (nazwa zapalenieOskrzeli)
                     (prawdopodobienstwo 15))))

(defrule drapanie-w-gardle ""

	(logical (drapanie-w-gardle Yes))
	
	=>
	
	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 12)))
	(assert (choroba (nazwa katarSienny)
						(prawdopodobienstwo 18)))
	(assert (choroba (nazwa niezyt)
						(prawdopodobienstwo 18)))
	(assert (choroba (nazwa astma)
						(prawdopodobienstwo 20))))

(defrule oslabienie ""

	(logical (oslabienie Yes))
	
	=>
	
	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 10)))
	(assert (choroba (nazwa niezyt)
						(prawdopodobienstwo 14)))
	(assert (choroba (nazwa katarSienny)
						(prawdopodobienstwo 14)))
	(assert (choroba (nazwa grypa)
						(prawdopodobienstwo 13))))

(defrule bole-kostno-stawowe ""

	(logical (bole-kostno-stawowe Yes))
	
	=>
	
	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 10)))
	(assert (choroba (nazwa grypa)
						(prawdopodobienstwo 13)))
	(assert (choroba (nazwa angina)
						(prawdopodobienstwo 14))))

(defrule zaczerwienienie ""

	(logical (zaczerwienienie Yes))
	
	=>
	
	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 10)))
	(assert (choroba (nazwa angina)
						(prawdopodobienstwo 19))))

(defrule chrypka ""

	(logical (chrypka Yes))

	=>

	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 18)))

	(assert (choroba (nazwa zapalenie)
						(prawdopodobienstwo 20))))

(defrule rozpulchnienie ""

	(logical (rozpulchnienie Yes))

	=>

	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 8)))

	(assert (choroba (nazwa angina)
						(prawdopodobienstwo 19))))

(defrule dyskomfort-w-krtani ""

	(logical (dyskomfort-w-krtani Yes))

	=>

	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 20)))

	(assert (choroba (nazwa zapalenie)
						(prawdopodobienstwo 30)))

	(assert (choroba (nazwa astma)
						(prawdopodobienstwo 20)))
	(assert (choroba (nazwa zapalenieOskrzeli)
						(prawdopodobienstwo 10))))

(defrule suchy-kaszel ""

	(logical (suchy-kaszel Yes))

		=>

	(assert (choroba (nazwa astma)
						(prawdopodobienstwo 42)))
	
	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 20))))

(defrule swiszczacy-oddech ""

	(logical (swiszczacy-oddech Yes))

		=>

	(assert (choroba (nazwa zapalenieOskrzeli)
						(prawdopodobienstwo 50))))

(defrule dreszcze ""

	(logical (dreszcze Yes))

	=>

	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 15)))

	(assert (choroba (nazwa angina)
						(prawdopodobienstwo 4))))

(defrule brak-apetytu ""

	(logical (brak-apetytu Yes))

		=>

	(assert (choroba (nazwa inna)
						(prawdopodobienstwo 15)))

	(assert (choroba (nazwa angina)
						(prawdopodobienstwo 4))))



;; kombinacja prawdopodobienstw

(defrule combine-certainties ""
  (declare (salience 100)
           (auto-focus TRUE))
  ?rem1 <- (choroba (nazwa ?rel) (prawdopodobienstwo ?per1))
  ?rem2 <- (choroba (nazwa ?rel) (prawdopodobienstwo ?per2))
  (test (neq ?rem1 ?rem2))
  =>
  (retract ?rem1)
  (modify ?rem2 (prawdopodobienstwo (/ (- (* 100 (+ ?per1 ?per2)) (* ?per1 ?per2)) 100))))


;; szukanie max

(defrule find-max-value

   (declare (salience 80))

   (choroba (nazwa ?name1) (prawdopodobienstwo ?value1&:(> ?value1 40)))
   (not (choroba (prawdopodobienstwo ?value2&:(> ?value2 ?value1))))
   
   =>
   (assert (max ?name1)))


;; wyswietlanie wyniku

(defrule normal-state-conclusions ""

   (logical (max none))
   
   =>

   (assert (UI-state (display NoRepair)
                     (state final))))

(defrule angina ""

   (logical (max angina))
   
   =>

   (assert (UI-state (display Angina)
                     (state final))))


(defrule grypa ""

   (logical (max grypa))
   
   =>

   (assert (UI-state (display Grypa)
                     (state final))))


(defrule niezyt ""

  (logical (max niezyt))
   
   =>

   (assert (UI-state (display NiezytNosa)
                     (state final))))


(defrule zapalenie ""

  (logical (max zapalenie))
   
   =>

   (assert (UI-state (display ZapalenieKrtani)
                     (state final))))

(defrule katarSienny ""

  (logical (max katarSienny))
   
   =>

   (assert (UI-state (display KatarSienny)
                     (state final))))



(defrule astma ""

  (logical (max astma))
   
   =>

   (assert (UI-state (display Astma)
                     (state final))))


(defrule zapalenieOskrzeli ""

  (logical (max zapalenieOskrzeli))
   
   =>

   (assert (UI-state (display ZapalenieOskrzeli)
                     (state final))))


(defrule inne ""

  (logical (max inna))
   
   =>

   (assert (UI-state (display InnaChoroba)
                     (state final))))

(defrule malo-danych ""

  (logical (max unknown))
   
   =>

   (assert (UI-state (display MaloDanych)
                     (state final))))


;;;*************************
;;;* GUI INTERACTION RULES *
;;;*************************

(defrule ask-question

   (declare (salience 5))
   
   (UI-state (id ?id))
   
   ?f <- (state-list (sequence $?s&:(not (member$ ?id ?s))))
             
   =>
   
   (modify ?f (current ?id)
              (sequence ?id ?s))
   
   (halt))

(defrule handle-next-no-change-none-middle-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id)

   ?f2 <- (state-list (current ?id) (sequence $? ?nid ?id $?))
                      
   =>
      
   (retract ?f1)
   
   (modify ?f2 (current ?nid))
   
   (halt))

(defrule handle-next-response-none-end-of-chain

   (declare (salience 10))
   
   ?f <- (next ?id)

   (state-list (sequence ?id $?))
   
   (UI-state (id ?id)
             (relation-asserted ?relation))
                   
   =>
      
   (retract ?f)

   (assert (add-response ?id))
   (assert (max unknown)))   

(defrule handle-next-no-change-middle-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id ?response)

   ?f2 <- (state-list (current ?id) (sequence $? ?nid ?id $?))
     
   (UI-state (id ?id) (response ?response))
   
   =>
      
   (retract ?f1)
   
   (modify ?f2 (current ?nid))
   
   (halt))

(defrule handle-next-change-middle-of-chain

   (declare (salience 10))
   
   (next ?id ?response)

   ?f1 <- (state-list (current ?id) (sequence ?nid $?b ?id $?e))
     
   (UI-state (id ?id) (response ~?response))
   
   ?f2 <- (UI-state (id ?nid))
   
   =>
         
   (modify ?f1 (sequence ?b ?id ?e))
   
   (retract ?f2))
   
(defrule handle-next-response-end-of-chain

   (declare (salience 10))
   
   ?f1 <- (next ?id ?response)
   
   (state-list (sequence ?id $?))
   
   ?f2 <- (UI-state (id ?id)
                    (response ?expected)
                    (relation-asserted ?relation))
                
   =>
      
   (retract ?f1)

   (if (neq ?response ?expected)
      then
      (modify ?f2 (response ?response)))
      
   (assert (add-response ?id ?response))
   (assert (max unknown)))   

(defrule handle-add-response

   (declare (salience 10))
   
   (logical (UI-state (id ?id)
                      (relation-asserted ?relation)))
   
   ?f1 <- (add-response ?id ?response)
                
   =>
      
   (str-assert (str-cat "(" ?relation " " ?response ")"))
   
   (retract ?f1))   

(defrule handle-add-response-none

   (declare (salience 10))
   
   (logical (UI-state (id ?id)
                      (relation-asserted ?relation)))
   
   ?f1 <- (add-response ?id)
                
   =>
      
   (str-assert (str-cat "(" ?relation ")"))
   
   (retract ?f1))   

(defrule handle-prev

   (declare (salience 10))
      
   ?f1 <- (prev ?id)
   
   ?f2 <- (state-list (sequence $?b ?id ?p $?e))
                
   =>
   
   (retract ?f1)
   
   (modify ?f2 (current ?p))
   
   (halt))
   
